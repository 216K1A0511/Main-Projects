using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Infrastructure.Persistence;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuotesController : ControllerBase
{
    private readonly AppDbContext _context;

    public QuotesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Quotes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Quote>>> GetQuotes()
    {
        return await _context.Quotes.ToListAsync();
    }

    // GET: api/Quotes/random
    [HttpGet("random")]
    public async Task<ActionResult<Quote>> GetRandomQuote()
    {
        var activeQuotes = await _context.Quotes.Where(q => q.IsActive).ToListAsync();
        if (!activeQuotes.Any())
        {
            return NotFound("No active quotes available.");
        }

        var random = new Random();
        var randomQuote = activeQuotes[random.Next(activeQuotes.Count)];

        return randomQuote;
    }

    // GET: api/Quotes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Quote>> GetQuote(Guid id)
    {
        var quote = await _context.Quotes.FindAsync(id);

        if (quote == null)
        {
            return NotFound();
        }

        return quote;
    }

    // PUT: api/Quotes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutQuote(Guid id, Quote quote)
    {
        if (id != quote.Id)
        {
            return BadRequest();
        }

        _context.Entry(quote).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!QuoteExists(id))
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

    // POST: api/Quotes
    [HttpPost]
    public async Task<ActionResult<Quote>> PostQuote(Quote quote)
    {
        quote.Id = Guid.NewGuid();
        if (quote.CreatedAt == default)
        {
            quote.CreatedAt = DateTime.UtcNow;
        }

        _context.Quotes.Add(quote);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetQuote", new { id = quote.Id }, quote);
    }

    // DELETE: api/Quotes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuote(Guid id)
    {
        var quote = await _context.Quotes.FindAsync(id);
        if (quote == null)
        {
            return NotFound();
        }

        _context.Quotes.Remove(quote);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool QuoteExists(Guid id)
    {
        return _context.Quotes.Any(e => e.Id == id);
    }
}
