using IDEAL.ERP.Domain.Common;

namespace IDEAL.ERP.Domain.Entities;

public class ProgramDetail : BaseEntity
{
    public string Name { get; set; } = string.Empty; // e.g., "Computer Science and Engineering (CSE)"
    public string Code { get; set; } = string.Empty; // e.g., "CSE"
    public int DurationYears { get; set; }
    public int TotalSemesters { get; set; }
    public int TotalSeats { get; set; }
    public int FilledSeats { get; set; }

    public int AvailableSeats => TotalSeats - FilledSeats;

    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}
