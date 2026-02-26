using IDEAL.ERP.Domain.Common;

namespace IDEAL.ERP.Domain.Entities;

public class Institution : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();
}

public class Student : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string RollNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    public Guid InstitutionId { get; set; }
    public Institution Institution { get; set; } = null!;
}

public class Faculty : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmployeeId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    public Guid InstitutionId { get; set; }
    public Institution Institution { get; set; } = null!;
}
