using Gmail.Helpers.CommonDto;
using Gmail.Helpers.Enums;

namespace Gmail.Application.Dto.Recipient;

public class RecipientDto : BaseDto
{
    public long EmailId { get; set; }
    public long UserId { get; set; } // UserId of the recipient
    public RecipientType Type { get; set; } // To, CC, BCC
    public bool HasRead { get; set; }
    public DateTime DateRead { get; set; }
    public string DisplayDateRead { get; set; } = string.Empty;
    public DateTime DateReceived { get; set; } // Date when the recipient received the email
    public string DisplayDateReceived { get; set; } = string.Empty; // Display date when the recipient received the email
    public string RecipientEmail { get; set; } = string.Empty; // Email address of the recipient
    public bool IsStarred { get; set; } // Indicates if the recipient has starred the email
    public bool IsArchived { get; set; } // Indicates if the recipient has archived the email
    public bool IsDeleted { get; set; } // Indicates if the recipient has deleted the email
}

