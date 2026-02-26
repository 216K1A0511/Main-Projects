using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Infrastructure.Persistence;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgramDetailsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProgramDetailsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/ProgramDetails
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProgramDetail>>> GetProgramDetails()
    {
        return await _context.ProgramDetails.ToListAsync();
    }

    // GET: api/ProgramDetails/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProgramDetail>> GetProgramDetail(Guid id)
    {
        var programDetail = await _context.ProgramDetails.FindAsync(id);

        if (programDetail == null)
        {
            return NotFound();
        }

        return programDetail;
    }

    // GET: api/ProgramDetails/{id}/seats
    // Check for program seats availability
    [HttpGet("{id}/seats")]
    public async Task<ActionResult<object>> GetProgramSeatsAvailability(Guid id)
    {
        var program = await _context.ProgramDetails.FindAsync(id);

        if (program == null)
        {
            return NotFound();
        }

        return new
        {
            program.Id,
            program.Name,
            program.Code,
            program.TotalSeats,
            program.FilledSeats,
            AvailableSeats = program.AvailableSeats
        };
    }

    // PUT: api/ProgramDetails/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProgramDetail(Guid id, ProgramDetail programDetail)
    {
        if (id != programDetail.Id)
        {
            return BadRequest();
        }

        _context.Entry(programDetail).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProgramDetailExists(id))
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

    // POST: api/ProgramDetails
    [HttpPost]
    public async Task<ActionResult<ProgramDetail>> PostProgramDetail(ProgramDetail programDetail)
    {
        programDetail.Id = Guid.NewGuid();
        if (programDetail.CreatedAt == default)
        {
            programDetail.CreatedAt = DateTime.UtcNow;
        }

        _context.ProgramDetails.Add(programDetail);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProgramDetail), new { id = programDetail.Id }, programDetail);
    }

    // DELETE: api/ProgramDetails/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProgramDetail(Guid id)
    {
        var programDetail = await _context.ProgramDetails.FindAsync(id);
        if (programDetail == null)
        {
            return NotFound();
        }

        _context.ProgramDetails.Remove(programDetail);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProgramDetailExists(Guid id)
    {
        return _context.ProgramDetails.Any(e => e.Id == id);
    }
}
