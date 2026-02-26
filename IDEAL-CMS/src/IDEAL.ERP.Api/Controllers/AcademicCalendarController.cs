using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Infrastructure.Persistence;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AcademicCalendarController : ControllerBase
{
    private readonly AppDbContext _context;

    public AcademicCalendarController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/AcademicCalendar
    // Date-wise list of already created Day-orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AcademicCalendarEntry>>> GetAcademicCalendarEntries()
    {
        return await _context.AcademicCalendarEntries
            .OrderBy(e => e.Date)
            .ToListAsync();
    }

    // GET: api/AcademicCalendar/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AcademicCalendarEntry>> GetAcademicCalendarEntry(Guid id)
    {
        var entry = await _context.AcademicCalendarEntries.FindAsync(id);

        if (entry == null)
        {
            return NotFound();
        }

        return entry;
    }

    // POST: api/AcademicCalendar
    [HttpPost]
    public async Task<ActionResult<AcademicCalendarEntry>> PostAcademicCalendarEntry(AcademicCalendarEntry entry)
    {
        entry.Id = Guid.NewGuid();
        if (entry.CreatedAt == default)
        {
            entry.CreatedAt = DateTime.UtcNow;
        }

        _context.AcademicCalendarEntries.Add(entry);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAcademicCalendarEntry), new { id = entry.Id }, entry);
    }

    // PUT: api/AcademicCalendar/5
    // User can Edit particular day "Day-order"
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAcademicCalendarEntry(Guid id, AcademicCalendarEntry entry)
    {
        if (id != entry.Id)
        {
            return BadRequest();
        }

        _context.Entry(entry).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AcademicCalendarEntryExists(id))
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

    // DELETE: api/AcademicCalendar/5
    // User can Delete particular day "Day-order"
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAcademicCalendarEntry(Guid id)
    {
        var entry = await _context.AcademicCalendarEntries.FindAsync(id);
        if (entry == null)
        {
            return NotFound();
        }

        _context.AcademicCalendarEntries.Remove(entry);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/AcademicCalendar/template
    // Download the day-order CSV template
    [HttpGet("template")]
    public IActionResult DownloadTemplate()
    {
        var csvContent = "Date,DayOrder,TargetBranch,Description\n2026-03-01,Day 1,All,First day of month\n2026-03-02,Day 2,CSE,Specific branch day order";
        var bytes = System.Text.Encoding.UTF8.GetBytes(csvContent);
        return File(bytes, "text/csv", "DayOrderTemplate.csv");
    }

    // POST: api/AcademicCalendar/import
    // Import "Day-orders" from a CSV file
    [HttpPost("import")]
    public async Task<IActionResult> ImportTemplate(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Please upload a valid CSV file.");
        }

        var entries = new List<AcademicCalendarEntry>();

        try
        {
            using (var stream = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(stream, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true }))
            {
                var records = csv.GetRecords<AcademicCalendarCsvRecord>().ToList();
                foreach (var record in records)
                {
                    if (DateTime.TryParse(record.Date, out DateTime parsedDate))
                    {
                        entries.Add(new AcademicCalendarEntry
                        {
                            Id = Guid.NewGuid(),
                            Date = parsedDate.ToUniversalTime(),
                            DayOrder = record.DayOrder ?? string.Empty,
                            TargetBranch = string.IsNullOrWhiteSpace(record.TargetBranch) ? "All" : record.TargetBranch,
                            Description = record.Description,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }
            }

            if (!entries.Any())
            {
                return BadRequest("No valid entries found in the file.");
            }

            _context.AcademicCalendarEntries.AddRange(entries);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"{entries.Count} Day-orders successfully imported." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while parsing the file: {ex.Message}");
        }
    }

    private bool AcademicCalendarEntryExists(Guid id)
    {
        return _context.AcademicCalendarEntries.Any(e => e.Id == id);
    }
}

public class AcademicCalendarCsvRecord
{
    public string Date { get; set; }
    public string DayOrder { get; set; }
    public string TargetBranch { get; set; }
    public string Description { get; set; }
}
