using Gmail.Helpers.CommonDto;

namespace Gmail.Application.Dto.Contact;

public class ContactDto : BaseDto
{
    public long UserId { get; set; }
    public string ContactName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    public string DisplayDateAdded { get; set; } = string.Empty;
    public string? Notes { get; set; } = string.Empty;
}
