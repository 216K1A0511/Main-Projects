using MediatR;
using Microsoft.AspNetCore.Mvc;
using IDEAL.ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstitutionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public InstitutionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _context.Institutions
            .Select(i => new { i.Id, i.Name, i.Code })
            .ToListAsync();
        return Ok(result);
    }
}

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    [HttpPost("change-password")]
    public IActionResult ChangePassword([FromBody] dynamic model)
    {
        // Placeholder for password change logic
        return Ok(new { message = "Password changed successfully" });
    }

    [HttpPost("upload-picture")]
    public IActionResult UploadPicture()
    {
        // Placeholder for profile picture upload logic
        return Ok(new { message = "Profile picture updated" });
    }
}
