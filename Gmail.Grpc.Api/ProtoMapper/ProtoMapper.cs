using AutoMapper;

namespace Gmail.Grpc.Api.ProtoMapper;

public class ProtoMapper : Profile
{
    public ProtoMapper()
    {
        CreateMap<Gmail.Application.Dto.Contact.ContactDto,
            ContactModel.ContactDto>().ReverseMap();

        CreateMap<Gmail.Application.Dto.Email.EmailDto,
            EmailModel.EmailDto>()
            .ForMember(dest => dest.Recipients, opt => opt.MapFrom(src => src.Recipients))
            .ReverseMap();

        CreateMap<Gmail.Application.Dto.Folder.FolderDto,
            FolderModel.FolderDto>()
            .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails))
            .ReverseMap();

        CreateMap<Gmail.Application.Dto.Recipient.RecipientDto,
            RecipientModel.RecipientDto>().ReverseMap();

        CreateMap<Gmail.Application.Dto.User.UserDto, UserModel.UserDto>()
            .ForMember(dest => dest.Folders, opt => opt.MapFrom(src => src.Folders))
            .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts))
            .ReverseMap();
    }
}
