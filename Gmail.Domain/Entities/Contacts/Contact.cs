using Gmail.Helpers.CommonEntity;

namespace Gmail.Domain.Entities.Contacts;

public class Contact : BaseEntity
{
    public long UserId { get; set; }
    public string ContactName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; }
    public string? Notes { get; set; } = string.Empty;
}
