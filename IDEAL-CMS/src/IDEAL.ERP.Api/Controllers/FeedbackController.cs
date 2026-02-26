using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Infrastructure.Persistence;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly AppDbContext _context;

    public FeedbackController(AppDbContext context)
    {
        _context = context;
    }

    // --- Feedback Parameters ---

    // GET: api/Feedback/parameters
    // Accessible by: Admin, HOD, Employee, Student
    [HttpGet("parameters")]
    public async Task<ActionResult<IEnumerable<FeedbackParameter>>> GetFeedbackParameters([FromQuery] FeedbackTargetType? type)
    {
        var query = _context.FeedbackParameters.AsQueryable();
        
        if (type.HasValue)
        {
            query = query.Where(p => p.TargetType == type.Value);
        }

        return await query.ToListAsync();
    }

    // POST: api/Feedback/parameters
    // Accessible by: Admin, HOD
    [HttpPost("parameters")]
    public async Task<ActionResult<FeedbackParameter>> PostFeedbackParameter(FeedbackParameter parameter)
    {
        parameter.Id = Guid.NewGuid();
        if (parameter.CreatedAt == default)
        {
            parameter.CreatedAt = DateTime.UtcNow;
        }

        _context.FeedbackParameters.Add(parameter);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFeedbackParameters), new { id = parameter.Id }, parameter);
    }

    // DELETE: api/Feedback/parameters/5
    // Accessible by: Admin, HOD
    [HttpDelete("parameters/{id}")]
    public async Task<IActionResult> DeleteFeedbackParameter(Guid id)
    {
        var parameter = await _context.FeedbackParameters.FindAsync(id);
        if (parameter == null) return NotFound();

        _context.FeedbackParameters.Remove(parameter);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    // --- Grievance Feedbacks ---

    // GET: api/Feedback
    // Chairman: Can view ALL feedbacks.
    // Employee/HOD/Student: Can generally view feedbacks they received/gave.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GrievanceFeedback>>> GetFeedbacks()
    {
        // NOTE: In a real app with JWT, you would extract the User Role here.
        // If Role == "Chairman", return all.
        // Else, return feedbacks where GivenToId == User.Id
        
        return await _context.GrievanceFeedbacks
            .Include(f => f.Parameter)
            .ToListAsync();
    }

    // POST: api/Feedback
    // Accessible by: Employee, HOD, Student
    [HttpPost]
    public async Task<ActionResult<GrievanceFeedback>> PostFeedback(GrievanceFeedback feedback)
    {
        // Parameter validation
        var parameter = await _context.FeedbackParameters.FindAsync(feedback.ParameterId);
        if (parameter == null || !parameter.IsActive)
        {
            return BadRequest("Invalid or inactive Feedback Parameter.");
        }

        // Logic Check: Ensure TargetType matches the GivenToRole
        // Example: If Parameter is for 'Student', then 'GivenToRole' MUST be 'Student'.
        if (parameter.TargetType == FeedbackTargetType.Student && !feedback.GivenToRole.Equals("Student", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("This parameter can only be used to evaluate Students.");
        }
        if (parameter.TargetType == FeedbackTargetType.Employee && !feedback.GivenToRole.Equals("Employee", StringComparison.OrdinalIgnoreCase) && !feedback.GivenToRole.Equals("HOD", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("This parameter can only be used to evaluate Employees or HODs.");
        }

        feedback.Id = Guid.NewGuid();
        if (feedback.CreatedAt == default)
        {
            feedback.CreatedAt = DateTime.UtcNow;
        }

        _context.GrievanceFeedbacks.Add(feedback);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFeedbacks), new { id = feedback.Id }, feedback);
    }
}
