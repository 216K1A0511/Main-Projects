using IDEAL.ERP.Domain.Common;

namespace IDEAL.ERP.Domain.Entities;

public class ComplaintParameter : BaseEntity
{
    public string Name { get; set; } = string.Empty; // e.g., "Indiscipline", "Harassment"
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}
