using IDEAL.ERP.Domain.Common;

namespace IDEAL.ERP.Domain.Entities;

// 1. Module Management
public class AppModule : BaseEntity
{
    public string Name { get; set; } = string.Empty; // e.g., "Academic", "Transport", "Hostel"
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true; // For activating/deactivating pre-defined modules
}

// 2. Academic Branch Configuration
public class AcademicBranch : BaseEntity
{
    public Guid InstitutionId { get; set; } // Links back to the specific Institution
    
    public string Name { get; set; } = string.Empty; // e.g., "Computer Science and Engineering"
    public string Code { get; set; } = string.Empty; // e.g., "CSE"
}

// 3. Batch Configuration
public class AcademicBatch : BaseEntity
{
    public Guid BranchId { get; set; }
    public AcademicBranch Branch { get; set; } = null!;

    public string Name { get; set; } = string.Empty; // e.g., "CSE (2017-2020)"
    public int StartYear { get; set; }
    public int EndYear { get; set; }
}

// 4. Groups Configuration
public class AcademicGroup : BaseEntity
{
    public Guid BatchId { get; set; }
    public AcademicBatch Batch { get; set; } = null!;

    public string SectionName { get; set; } = string.Empty; // e.g., "Sec-A"
    public int SectionCapacity { get; set; } // e.g., 60

    public string GroupName { get; set; } = string.Empty; // e.g., "Group-1"
    public int GroupCapacity { get; set; } // e.g., 20
}

// 5. Group Assignments
public class StudentGroupAssignment : BaseEntity
{
    public Guid StudentId { get; set; }
    public Guid AcademicGroupId { get; set; }
    public AcademicGroup AcademicGroup { get; set; } = null!;
}
