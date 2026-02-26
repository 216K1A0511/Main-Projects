using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDEAL.ERP.Domain.Entities;
using IDEAL.ERP.Infrastructure.Persistence;

namespace IDEAL.ERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeWorkingDaysController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeeWorkingDaysController(AppDbContext context)
    {
        _context = context;
    }

    // --- Official Working Days Configuration ---

    // GET: api/EmployeeWorkingDays/setup
    [HttpGet("setup")]
    public async Task<ActionResult<IEnumerable<EmployeeWorkingDay>>> GetWorkingDays([FromQuery] int? month, [FromQuery] int? year)
    {
        var query = _context.EmployeeWorkingDays.AsQueryable();

        if (month.HasValue && year.HasValue)
        {
            query = query.Where(w => w.Date.Month == month.Value && w.Date.Year == year.Value);
        }

        return await query.OrderBy(w => w.Date).ToListAsync();
    }

    // POST: api/EmployeeWorkingDays/setup
    [HttpPost("setup")]
    public async Task<ActionResult<EmployeeWorkingDay>> SetWorkingDay(EmployeeWorkingDay workingDay)
    {
        var existing = await _context.EmployeeWorkingDays.FirstOrDefaultAsync(w => w.Date.Date == workingDay.Date.Date);
        if (existing != null)
        {
            existing.IsWorkingDay = workingDay.IsWorkingDay;
            existing.Description = workingDay.Description;
        }
        else
        {
            workingDay.Id = Guid.NewGuid();
            workingDay.CreatedAt = DateTime.UtcNow;
            _context.EmployeeWorkingDays.Add(workingDay);
        }

        await _context.SaveChangesAsync();
        return Ok(workingDay);
    }


    // --- Daily Attendance Transaction Log ---

    // POST: api/EmployeeWorkingDays/attendance
    [HttpPost("attendance")]
    public async Task<IActionResult> MarkAttendance(EmployeeAttendance attendance)
    {
        // 1. Validation: Prevent attendance if the date is a designated holiday
        var isHoliday = await _context.Holidays.AnyAsync(h => 
            attendance.Date.Date >= h.StartDate.Date && attendance.Date.Date <= h.EndDate.Date);

        if (isHoliday)
        {
            return BadRequest("Attendance cannot be marked because the selected date is a designated Holiday.");
        }

        // 2. Process Attendance
        var existing = await _context.EmployeeAttendances
            .FirstOrDefaultAsync(a => a.EmployeeId == attendance.EmployeeId && a.Date.Date == attendance.Date.Date);

        if (existing != null)
        {
            existing.IsPresent = attendance.IsPresent;
            existing.LeaveType = attendance.LeaveType;
            existing.Remarks = attendance.Remarks;
        }
        else
        {
            attendance.Id = Guid.NewGuid();
            attendance.CreatedAt = DateTime.UtcNow;
            _context.EmployeeAttendances.Add(attendance);
        }

        await _context.SaveChangesAsync();
        return Ok(attendance);
    }

    // --- Analytics: Calculate Attendance Status ---
    
    // GET: api/EmployeeWorkingDays/analytics/{employeeId}
    // Automatically calculates attendance rating based on total official working days and the employee's presence log.
    [HttpGet("analytics/{employeeId}")]
    public async Task<ActionResult> GetEmployeeAttendanceStats(Guid employeeId, [FromQuery] int month, [FromQuery] int year)
    {
        // 1. Get total official working days in that month
        var totalWorkingDays = await _context.EmployeeWorkingDays
            .CountAsync(w => w.Date.Month == month && w.Date.Year == year && w.IsWorkingDay);

        // 2. Get total days the employee was present in that month
        var totalPresent = await _context.EmployeeAttendances
            .CountAsync(a => a.EmployeeId == employeeId && a.Date.Month == month && a.Date.Year == year && a.IsPresent);

        // Calculate attendance decimal
        double attendancePercentage = totalWorkingDays > 0 
            ? ((double)totalPresent / totalWorkingDays) * 100 
            : 0;

        return Ok(new
        {
            EmployeeId = employeeId,
            Month = month,
            Year = year,
            TotalOfficialWorkingDays = totalWorkingDays,
            TotalDaysPresent = totalPresent,
            AttendancePercentage = Math.Round(attendancePercentage, 2)
        });
    }
}
