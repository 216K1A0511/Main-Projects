using IDEAL.ERP.Domain.Common;

namespace IDEAL.ERP.Domain.Entities;

// Setup table for defining which calendar days are official working days for the staff.
// Required to calculate accurate attendance percentages (Total Present / Total Working Days).
public class EmployeeWorkingDay : BaseEntity
{
    public DateTime Date { get; set; }
    public bool IsWorkingDay { get; set; } = true;
    public string? Description { get; set; } // e.g., "Regular Working Day", "Weekend", "Public Holiday"
}

// Transactional table to capture daily employee attendance.
public class EmployeeAttendance : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public bool IsPresent { get; set; }
    public string? LeaveType { get; set; } // null if present, "Sick", "Casual", etc. if absent
    public string? Remarks { get; set; }
}
