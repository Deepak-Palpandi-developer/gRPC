using Gmail.Application.Dto.Email;
using Gmail.Helpers.CommonDto;

namespace Gmail.Application.Dto.Folder;

public class FolderDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public long UserId { get; set; }
    public List<EmailDto>? Emails { get; set; } = new List<EmailDto>();
    public DateTime DateCreated { get; set; }
    public string DisplayDateCreated { get; set; } = string.Empty;
    public bool IsDefault { get; set; } // Indicates if the folder is a default system folder
    public DateTime LastModified { get; set; } // Last modified date of the folder
    public string DisplayLastModified { get; set; } = string.Empty;
    public int EmailCount { get; set; } // Number of emails in the folder
    public bool IsArchived { get; set; } // Indicates if the folder is archived
    public bool IsShared { get; set; } // Indicates if the folder is shared with other users
}

