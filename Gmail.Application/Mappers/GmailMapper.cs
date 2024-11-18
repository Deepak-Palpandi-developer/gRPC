using AutoMapper;
using Gmail.Application.Dto.Contact;
using Gmail.Application.Dto.Email;
using Gmail.Application.Dto.Folder;
using Gmail.Application.Dto.Recipient;
using Gmail.Application.Dto.User;
using Gmail.Domain.Entities.Contacts;
using Gmail.Domain.Entities.Emails;
using Gmail.Domain.Entities.Folders;
using Gmail.Domain.Entities.Recipients;
using Gmail.Domain.Entities.Users;

namespace Gmail.Application.Mappers;

public class GmailMapper : Profile
{
    public GmailMapper()
    {
        CreateMap<Contact, ContactDto>().ReverseMap();
        CreateMap<Email, EmailDto>().ReverseMap();
        CreateMap<Folder, FolderDto>().ReverseMap();
        CreateMap<Recipient, RecipientDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}