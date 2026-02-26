using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Infrastructure.Persistence;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintParametersController : ControllerBase
{
    private readonly AppDbContext _context;

    public ComplaintParametersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/ComplaintParameters
    // Accessible by: Employee, HOD, Student, Principal, Chairman
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComplaintParameter>>> GetComplaintParameters()
    {
        return await _context.ComplaintParameters.ToListAsync();
    }

    // GET: api/ComplaintParameters/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ComplaintParameter>> GetComplaintParameter(Guid id)
    {
        var parameter = await _context.ComplaintParameters.FindAsync(id);

        if (parameter == null)
        {
            return NotFound();
        }

        return parameter;
    }

    // POST: api/ComplaintParameters
    // Accessible only by: Employee, HOD, Student
    [HttpPost]
    public async Task<ActionResult<ComplaintParameter>> PostComplaintParameter(ComplaintParameter parameter)
    {
        parameter.Id = Guid.NewGuid();
        if (parameter.CreatedAt == default)
        {
            parameter.CreatedAt = DateTime.UtcNow;
        }

        _context.ComplaintParameters.Add(parameter);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetComplaintParameter), new { id = parameter.Id }, parameter);
    }

    // PUT: api/ComplaintParameters/5
    // Accessible only by: Employee, HOD, Student
    [HttpPut("{id}")]
    public async Task<IActionResult> PutComplaintParameter(Guid id, ComplaintParameter parameter)
    {
        if (id != parameter.Id)
        {
            return BadRequest();
        }

        _context.Entry(parameter).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ComplaintParameterExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/ComplaintParameters/5
    // Accessible only by: Employee, HOD, Student
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComplaintParameter(Guid id)
    {
        var parameter = await _context.ComplaintParameters.FindAsync(id);
        if (parameter == null)
        {
            return NotFound();
        }

        _context.ComplaintParameters.Remove(parameter);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ComplaintParameterExists(Guid id)
    {
        return _context.ComplaintParameters.Any(e => e.Id == id);
    }
}
