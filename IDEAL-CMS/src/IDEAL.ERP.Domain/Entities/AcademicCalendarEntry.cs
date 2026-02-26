using IDEAL.ERP.Domain.Common;

namespace IDEAL.ERP.Domain.Entities;

public class AcademicCalendarEntry : BaseEntity
{
    public DateTime Date { get; set; }
    public string DayOrder { get; set; } = string.Empty; // e.g., "Day 1", "Day 2", etc.
    public string TargetBranch { get; set; } = "All"; // "All" for all branches, or specific like "CSE"
    public string? Description { get; set; }
}
