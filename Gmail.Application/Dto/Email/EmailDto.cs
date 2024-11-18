using Gmail.Application.Dto.Recipient;
using Gmail.Helpers.CommonDto;

namespace Gmail.Application.Dto.Email;

public class EmailDto : BaseDto
{
    public long SenderId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime DateSent { get; set; }
    public string DisplayDateSent { get; set; } = string.Empty;
    public DateTime DateReceived { get; set; }
    public string DisplayDateReceived { get; set; } = string.Empty;
    public DateTime DateRead { get; set; }
    public string DisplayDateRead { get; set; } = string.Empty;
    public List<RecipientDto>? Recipients { get; set; } = new List<RecipientDto>();
    public bool IsRead { get; set; }
    public bool IsStarred { get; set; }
    public bool IsDraft { get; set; }
    public string? ImportanceLevel { get; set; } = string.Empty;
    public string? AttachmentPath { get; set; } = string.Empty;
    public List<string>? Tags { get; set; } = new List<string>();
    public bool HasAttachments { get; set; } // Indicates if the email has attachments
    public string MimeType { get; set; } = string.Empty;
    public string ReplyTo { get; set; } = string.Empty;
    public List<EmailDto>? ForwardedEmails { get; set; } = new List<EmailDto>();
    public bool IsSpam { get; set; }
    public bool IsArchived { get; set; }
}