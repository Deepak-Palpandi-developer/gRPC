using Gmail.Domain.Entities.Recipients;
using Gmail.Helpers.CommonEntity;

namespace Gmail.Domain.Entities.Emails;

public class Email : AuditEntity
{
    public long SenderId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime DateSent { get; set; }
    public DateTime DateReceived { get; set; }
    public DateTime DateRead { get; set; }
    public List<Recipient> Recipients { get; set; } = new List<Recipient>();
    public bool IsRead { get; set; }
    public bool IsStarred { get; set; }
    public bool IsDraft { get; set; }
    public string? ImportanceLevel { get; set; } = string.Empty;
    public string? AttachmentPath { get; set; } = string.Empty;
    public List<string>? Tags { get; set; } = new List<string>();
    public bool HasAttachments { get; set; } // Indicates if the email has attachments
    public string MimeType { get; set; } = string.Empty;
    public string ReplyTo { get; set; } = string.Empty;
    public List<Email> ForwardedEmails { get; set; } = new List<Email>();
    public bool IsSpam { get; set; }
    public bool IsArchived { get; set; }
}
