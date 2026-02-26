using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Infrastructure.Persistence;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SetupsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SetupsController(AppDbContext context)
    {
        _context = context;
    }

    // --- College Setup ---
    [HttpGet("college")]
    public async Task<ActionResult<IEnumerable<CollegeSetup>>> GetCollegeSetups()
    {
        return await _context.CollegeSetups.ToListAsync();
    }

    [HttpPost("college")]
    public async Task<ActionResult<CollegeSetup>> PostCollegeSetup(CollegeSetup collegeSetup)
    {
        collegeSetup.Id = Guid.NewGuid();
        if (collegeSetup.CreatedAt == default)
        {
            collegeSetup.CreatedAt = DateTime.UtcNow;
        }

        _context.CollegeSetups.Add(collegeSetup);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCollegeSetups), new { id = collegeSetup.Id }, collegeSetup);
    }

    [HttpPut("college/{id}")]
    public async Task<IActionResult> PutCollegeSetup(Guid id, CollegeSetup collegeSetup)
    {
        if (id != collegeSetup.Id) return BadRequest();
        _context.Entry(collegeSetup).State = EntityState.Modified;
        
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException) { if (!CollegeSetupExists(id)) return NotFound(); else throw; }
        return NoContent();
    }

    [HttpDelete("college/{id}")]
    public async Task<IActionResult> DeleteCollegeSetup(Guid id)
    {
        var setup = await _context.CollegeSetups.FindAsync(id);
        if (setup == null) return NotFound();
        _context.CollegeSetups.Remove(setup);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool CollegeSetupExists(Guid id) => _context.CollegeSetups.Any(e => e.Id == id);


    // --- Academic Setup ---
    [HttpGet("academic")]
    public async Task<ActionResult<IEnumerable<AcademicSetup>>> GetAcademicSetups()
    {
        return await _context.AcademicSetups.ToListAsync();
    }

    [HttpPost("academic")]
    public async Task<ActionResult<AcademicSetup>> PostAcademicSetup(AcademicSetup academicSetup)
    {
        academicSetup.Id = Guid.NewGuid();
        if (academicSetup.CreatedAt == default)
        {
            academicSetup.CreatedAt = DateTime.UtcNow;
        }

        _context.AcademicSetups.Add(academicSetup);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAcademicSetups), new { id = academicSetup.Id }, academicSetup);
    }

    [HttpPut("academic/{id}")]
    public async Task<IActionResult> PutAcademicSetup(Guid id, AcademicSetup academicSetup)
    {
        if (id != academicSetup.Id) return BadRequest();
        _context.Entry(academicSetup).State = EntityState.Modified;
        
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException) { if (!AcademicSetupExists(id)) return NotFound(); else throw; }
        return NoContent();
    }

    [HttpDelete("academic/{id}")]
    public async Task<IActionResult> DeleteAcademicSetup(Guid id)
    {
        var setup = await _context.AcademicSetups.FindAsync(id);
        if (setup == null) return NotFound();
        _context.AcademicSetups.Remove(setup);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool AcademicSetupExists(Guid id) => _context.AcademicSetups.Any(e => e.Id == id);


    // --- Extracurricular Setup ---
    [HttpGet("extracurricular")]
    public async Task<ActionResult<IEnumerable<ExtracurricularSetup>>> GetExtracurricularSetups()
    {
        return await _context.ExtracurricularSetups.ToListAsync();
    }

    [HttpPost("extracurricular")]
    public async Task<ActionResult<ExtracurricularSetup>> PostExtracurricularSetup(ExtracurricularSetup setup)
    {
        setup.Id = Guid.NewGuid();
        if (setup.CreatedAt == default)
        {
            setup.CreatedAt = DateTime.UtcNow;
        }

        _context.ExtracurricularSetups.Add(setup);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetExtracurricularSetups), new { id = setup.Id }, setup);
    }

    [HttpPut("extracurricular/{id}")]
    public async Task<IActionResult> PutExtracurricularSetup(Guid id, ExtracurricularSetup setup)
    {
        if (id != setup.Id) return BadRequest();
        _context.Entry(setup).State = EntityState.Modified;
        
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException) { if (!ExtracurricularSetupExists(id)) return NotFound(); else throw; }
        return NoContent();
    }

    [HttpDelete("extracurricular/{id}")]
    public async Task<IActionResult> DeleteExtracurricularSetup(Guid id)
    {
        var setup = await _context.ExtracurricularSetups.FindAsync(id);
        if (setup == null) return NotFound();
        _context.ExtracurricularSetups.Remove(setup);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ExtracurricularSetupExists(Guid id) => _context.ExtracurricularSetups.Any(e => e.Id == id);
}
