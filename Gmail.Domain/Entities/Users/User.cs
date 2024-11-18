using Gmail.Domain.Entities.Contacts;
using Gmail.Domain.Entities.Folders;
using Gmail.Helpers.CommonEntity;

namespace Gmail.Domain.Entities.Users;

public class User : AuditEntity
{
    public string Username { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public int BirthMonth { get; set; } 
    public int BirthDay { get; set; } 
    public int BirthYear { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PrefixPhoneNumber { get; set; } = string.Empty;
    public DateTime LastLogin { get; set; }
    public string? ProfilePictureUrl { get; set; } = string.Empty;
    public List<Folder> Folders { get; set; } = new List<Folder>();
    public List<Contact> Contacts { get; set; } = new List<Contact>();
}
