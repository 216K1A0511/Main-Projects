using IDEAL.ERP.Domain.Common;
using IDEAL.ERP.Domain.Enums;

namespace IDEAL.ERP.Domain.Entities;

public class TodoTask : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskPriority Priority { get; set; }
    public IDEAL.ERP.Domain.Enums.TaskStatus Status { get; set; } = IDEAL.ERP.Domain.Enums.TaskStatus.Pending;
    public DateTime? ReminderDateTime { get; set; }
    public Guid UserId { get; set; } // Associated with the Admin/User
}

public class JobApplication : BaseEntity
{
    public string ApplicantName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
}

public class PlacementDrive : BaseEntity
{
    public string CompanyName { get; set; } = string.Empty;
    public DateTime DriveDate { get; set; }
    public string? Description { get; set; }
    public string EligibleBatch { get; set; } = string.Empty;
}

public class Event : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class Notice : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsPublished { get; set; } = true;
}

public class Holiday : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
}

public class News : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}
