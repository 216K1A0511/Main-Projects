using IDEAL.ERP.Domain.Common;

namespace IDEAL.ERP.Domain.Entities;

public class Quote : BaseEntity
{
    public string Text { get; set; } = string.Empty;
    public string? Author { get; set; }
    public bool IsActive { get; set; } = true;
}

public class CollegeSetup : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Website { get; set; }
    public string? Affiliation { get; set; }
    public int? EstablishedYear { get; set; }
}

public class AcademicSetup : BaseEntity
{
    public string AcademicYear { get; set; } = string.Empty; // e.g., "2023-2024"
    public string Semester { get; set; } = string.Empty; // e.g., "Fall", "Spring"
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCurrent { get; set; }
}

public class ExtracurricularSetup : BaseEntity
{
    public string ActivityName { get; set; } = string.Empty;
    public string? Category { get; set; } // e.g., Sports, Arts, Club
    public string? Description { get; set; }
    public string? CoordinatorName { get; set; }
    public bool IsActive { get; set; } = true;
}






















































































































