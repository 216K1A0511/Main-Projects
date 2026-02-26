using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Infrastructure.Persistence;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AcademicStructureController : ControllerBase
{
    private readonly AppDbContext _context;

    public AcademicStructureController(AppDbContext context)
    {
        _context = context;
    }

    // --- 1. Branches ---
    [HttpGet("branches")]
    public async Task<ActionResult<IEnumerable<AcademicBranch>>> GetBranches([FromQuery] Guid? institutionId)
    {
        var query = _context.AcademicBranches.AsQueryable();
        if (institutionId.HasValue)
        {
            query = query.Where(b => b.InstitutionId == institutionId.Value);
        }
        return await query.ToListAsync();
    }

    [HttpPost("branches")]
    public async Task<ActionResult<AcademicBranch>> CreateBranch(AcademicBranch branch)
    {
        branch.Id = Guid.NewGuid();
        branch.CreatedAt = DateTime.UtcNow;

        _context.AcademicBranches.Add(branch);
        await _context.SaveChangesAsync();
        return Ok(branch);
    }
    
    [HttpPut("branches/{id}")]
    public async Task<IActionResult> UpdateBranch(Guid id, AcademicBranch branch)
    {
        if (id != branch.Id) return BadRequest();
        _context.Entry(branch).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }


    // --- 2. Batches ---
    [HttpGet("batches")]
    public async Task<ActionResult<IEnumerable<AcademicBatch>>> GetBatches([FromQuery] Guid? branchId)
    {
        var query = _context.AcademicBatches.Include(b => b.Branch).AsQueryable();
        if (branchId.HasValue)
        {
            query = query.Where(b => b.BranchId == branchId.Value);
        }
        return await query.ToListAsync();
    }

    [HttpPost("batches")]
    public async Task<ActionResult<AcademicBatch>> CreateBatch(AcademicBatch batch)
    {
        var branch = await _context.AcademicBranches.FindAsync(batch.BranchId);
        if (branch == null) return BadRequest("Invalid Branch ID");

        batch.Id = Guid.NewGuid();
        batch.CreatedAt = DateTime.UtcNow;

        _context.AcademicBatches.Add(batch);
        await _context.SaveChangesAsync();
        return Ok(batch);
    }


    // --- 3. Groups ---
    [HttpGet("groups")]
    public async Task<ActionResult<IEnumerable<AcademicGroup>>> GetGroups([FromQuery] Guid? batchId)
    {
        var query = _context.AcademicGroups.Include(g => g.Batch).AsQueryable();
        if (batchId.HasValue)
        {
            query = query.Where(g => g.BatchId == batchId.Value);
        }
        return await query.ToListAsync();
    }

    [HttpPost("groups")]
    public async Task<ActionResult<AcademicGroup>> CreateGroup(AcademicGroup group)
    {
        var batch = await _context.AcademicBatches.FindAsync(group.BatchId);
        if (batch == null) return BadRequest("Invalid Batch ID");

        // Validate group capacity doesn't exceed section capacity blindly (if desired as a strict rule),
        // or just let it pass based on the prompt's structural guidance.

        group.Id = Guid.NewGuid();
        group.CreatedAt = DateTime.UtcNow;

        _context.AcademicGroups.Add(group);
        await _context.SaveChangesAsync();
        return Ok(group);
    }

    // --- 4. Group Assignments ---
    [HttpGet("groups/{groupId}/students")]
    public async Task<ActionResult<IEnumerable<object>>> GetStudentsInGroup(Guid groupId)
    {
        var assignments = await _context.StudentGroupAssignments
            .Where(a => a.AcademicGroupId == groupId)
            .Join(_context.Students, 
                  a => a.StudentId, 
                  s => s.Id, 
                  (a, s) => new { s.Id, s.FirstName, s.LastName, s.RollNumber })
            .ToListAsync();

        return Ok(assignments);
    }

    [HttpPost("groups/{groupId}/assign")]
    public async Task<IActionResult> AssignStudentsToGroup(Guid groupId, [FromBody] List<Guid> studentIds)
    {
        var group = await _context.AcademicGroups.FindAsync(groupId);
        if (group == null) return NotFound("Academic Group not found");

        // Remove existing assignments for these students in this specific group if needed,
        // but typically you'd just add new ones or handle duplicates.
        var existingAssignments = await _context.StudentGroupAssignments
            .Where(a => a.AcademicGroupId == groupId)
            .Select(a => a.StudentId)
            .ToListAsync();

        var newStudentIds = studentIds.Except(existingAssignments).ToList();

        // Check Capacity
        if (existingAssignments.Count + newStudentIds.Count > group.GroupCapacity)
        {
            return BadRequest($"Cannot assign. Exceeds group capacity of {group.GroupCapacity}");
        }

        var newAssignments = newStudentIds.Select(studentId => new StudentGroupAssignment
        {
            Id = Guid.NewGuid(),
            AcademicGroupId = groupId,
            StudentId = studentId,
            CreatedAt = DateTime.UtcNow
        });

        _context.StudentGroupAssignments.AddRange(newAssignments);
        await _context.SaveChangesAsync();

        return Ok(new { Message = $"{newStudentIds.Count} students successfully assigned to {group.GroupName}." });
    }

    [HttpDelete("groups/{groupId}/students/{studentId}")]
    public async Task<IActionResult> RemoveStudentFromGroup(Guid groupId, Guid studentId)
    {
        var assignment = await _context.StudentGroupAssignments
            .FirstOrDefaultAsync(a => a.AcademicGroupId == groupId && a.StudentId == studentId);

        if (assignment == null) return NotFound("Student not found in the group");

        _context.StudentGroupAssignments.Remove(assignment);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
