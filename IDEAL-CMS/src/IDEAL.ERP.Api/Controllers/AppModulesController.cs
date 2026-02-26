using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Infrastructure.Persistence;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppModulesController : ControllerBase
{
    private readonly AppDbContext _context;

    public AppModulesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppModule>>> GetModules()
    {
        return await _context.AppModules.OrderBy(m => m.Name).ToListAsync();
    }

    [HttpPost("initialize")]
    public async Task<IActionResult> InitializeDefaultModules()
    {
        var existing = await _context.AppModules.AnyAsync();
        if (!existing)
        {
            var defaultModules = new List<AppModule>
            {
                new AppModule { Name = "Academic", Description = "Manage branches, batches, groups, and academic structure", IsActive = true, Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow },
                new AppModule { Name = "Transport", Description = "Manage college transportation", IsActive = false, Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow },
                new AppModule { Name = "Hostel", Description = "Manage dormitories and residential living", IsActive = false, Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow },
                new AppModule { Name = "Library", Description = "Manage book inventory and issuances", IsActive = false, Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow }
            };

            _context.AppModules.AddRange(defaultModules);
            await _context.SaveChangesAsync();
            return Ok(defaultModules);
        }

        return Ok("Modules already initialized");
    }

    [HttpPut("{id}/toggle")]
    public async Task<IActionResult> ToggleModule(Guid id)
    {
        var module = await _context.AppModules.FindAsync(id);
        if (module == null) return NotFound();

        module.IsActive = !module.IsActive;
        await _context.SaveChangesAsync();
        
        return Ok(module);
    }
}
