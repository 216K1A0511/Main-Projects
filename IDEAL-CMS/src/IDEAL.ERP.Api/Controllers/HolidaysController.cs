using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Infrastructure.Persistence;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HolidaysController : ControllerBase
{
    private readonly AppDbContext _context;

    public HolidaysController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Holidays
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Holiday>>> GetHolidays()
    {
        return await _context.Holidays.OrderBy(h => h.StartDate).ToListAsync();
    }

    // GET: api/Holidays/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Holiday>> GetHoliday(Guid id)
    {
        var holiday = await _context.Holidays.FindAsync(id);

        if (holiday == null)
        {
            return NotFound();
        }

        return holiday;
    }

    // POST: api/Holidays
    [HttpPost]
    public async Task<ActionResult<Holiday>> PostHoliday(Holiday holiday)
    {
        // Support single day by copying StartDate if EndDate isn't provided or is invalid
        if (holiday.EndDate == default || holiday.EndDate < holiday.StartDate)
        {
            holiday.EndDate = holiday.StartDate;
        }

        holiday.Id = Guid.NewGuid();
        if (holiday.CreatedAt == default)
        {
            holiday.CreatedAt = DateTime.UtcNow;
        }

        _context.Holidays.Add(holiday);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHoliday), new { id = holiday.Id }, holiday);
    }

    // PUT: api/Holidays/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHoliday(Guid id, Holiday holiday)
    {
        if (id != holiday.Id)
        {
            return BadRequest();
        }

        if (holiday.EndDate == default || holiday.EndDate < holiday.StartDate)
        {
            holiday.EndDate = holiday.StartDate;
        }

        _context.Entry(holiday).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HolidayExists(id))
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

    // DELETE: api/Holidays/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHoliday(Guid id)
    {
        var holiday = await _context.Holidays.FindAsync(id);
        if (holiday == null)
        {
            return NotFound();
        }

        _context.Holidays.Remove(holiday);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool HolidayExists(Guid id)
    {
        return _context.Holidays.Any(e => e.Id == id);
    }
}
