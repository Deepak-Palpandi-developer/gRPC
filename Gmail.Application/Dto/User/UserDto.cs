using Gmail.Application.Dto.Contact;
using Gmail.Application.Dto.Folder;
using Gmail.Helpers.CommonDto;

namespace Gmail.Application.Dto.User;

public class UserDto : BaseDto
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
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
    public string DiplayLastLogin { get; set; } = string.Empty;
    public string BithDate { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; } = string.Empty;
    public List<FolderDto>? Folders { get; set; } = new List<FolderDto>();
    public List<ContactDto>? Contacts { get; set; } = new List<ContactDto>();
}