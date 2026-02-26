using IDEAL.ERP.Domain.Common;

namespace IDEAL.ERP.Domain.Entities;

public enum FeedbackTargetType
{
    Student,
    Employee
}

public class FeedbackParameter : BaseEntity
{
    public string Name { get; set; } = string.Empty; // e.g., "Punctuality", "Teaching Quality"
    public string? Description { get; set; }
    public FeedbackTargetType TargetType { get; set; } 
    public bool IsActive { get; set; } = true;
}

public class GrievanceFeedback : BaseEntity
{
    public Guid ParameterId { get; set; }
    public FeedbackParameter Parameter { get; set; } = null!;
    
    public string GivenByRole { get; set; } = string.Empty; // e.g., "Student", "Employee", "HOD"
    public Guid GivenById { get; set; } // The ID of the person giving the feedback

    public string GivenToRole { get; set; } = string.Empty; // e.g., "Student", "Employee" 
    public Guid GivenToId { get; set; } // The ID of the person receiving the feedback

    public string Content { get; set; } = string.Empty;
    public int? Rating { get; set; } // e.g., 1-5 scalar rating
}
