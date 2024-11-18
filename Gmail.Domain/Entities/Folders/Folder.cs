using Gmail.Domain.Entities.Emails;
using Gmail.Helpers.CommonEntity;

namespace Gmail.Domain.Entities.Folders;

public class Folder : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public long UserId { get; set; }
    public List<Email> Emails { get; set; } = new List<Email>();
    public DateTime DateCreated { get; set; }
    public bool IsDefault { get; set; } // Indicates if the folder is a default system folder
    public DateTime LastModified { get; set; } // Last modified date of the folder
    public int EmailCount { get; set; } // Number of emails in the folder
    public bool IsArchived { get; set; } // Indicates if the folder is archived
    public bool IsShared { get; set; } // Indicates if the folder is shared with other users
}