using IDEAL.ERP.Domain.Common;

namespace IDEAL.ERP.Domain.Entities;

public class EmailMessage : BaseEntity
{
    public string Sender { get; set; } = string.Empty;
    public string Receiver { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}
