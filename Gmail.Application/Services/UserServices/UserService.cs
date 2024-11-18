using AutoMapper;
using Gmail.Application.Dto.User;
using Gmail.Domain.Entities.Users;
using Gmail.Domain.Repository.UserRepositorys;
using Gmail.Helpers.CommonModels;
using Microsoft.AspNetCore.Http;

namespace Gmail.Application.Services.UserServices;

public interface IUserService
{
    Task<ResponseModel<List<UserDto>>> GetUser();
    Task<ResponseModel<UserDto?>> GetUser(long userId);
    Task<ResponseModel<UserDto?>> CreateUser(UserDto userDto);
    Task<ResponseModel<UserDto?>> Login(string email, string password);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    #region Public
    public async Task<ResponseModel<List<UserDto>>> GetUser()
    {
        var data = await _userRepository.GetAllUsersAsync();
        var mappedData = _mapper.Map<List<UserDto>>(data);

        if (mappedData != null && mappedData.Any())
        {
            foreach (var userDto in mappedData)
            {
                FormatUserDetails(userDto);
            }
        }

        return new ResponseModel<List<UserDto>>
        {
            IsSuccess = data != null && data.Any(),
            Message = data != null && data.Any() ? "Users fetched successfully" : "No users found",
            Data = mappedData ?? new List<UserDto>()
        };
    }

    public async Task<ResponseModel<UserDto?>> GetUser(long userId)
    {
        var data = await _userRepository.GetUserDetailsAsync(userId);

        var mappedData = data != null ? _mapper.Map<UserDto>(data) : null;

        if (mappedData != null)
        {
            FormatUserDetails(mappedData);
        }

        return new ResponseModel<UserDto?>
        {
            IsSuccess = data != null,
            Message = data != null ? "User details fetched successfully" : "User not found",
            Data = mappedData
        };
    }

    public async Task<ResponseModel<UserDto?>> CreateUser(UserDto userDto)
    {
        var userEntity = _mapper.Map<User>(userDto);

        if (userEntity == null) return new ResponseModel<UserDto?> { IsSuccess = false, Message = "Invalid user data" };

        SetDefaultSettings(userDto, userEntity);

        await _userRepository.CreateUserAsync(userEntity);

        var createdUser = await _userRepository.GetUserDetailsAsync(userEntity.Id);

        var mappedData = createdUser != null ? _mapper.Map<UserDto>(createdUser) : null;

        if (mappedData != null)
            FormatUserDetails(mappedData);

        return new ResponseModel<UserDto?>
        {
            IsSuccess = createdUser != null,
            Message = createdUser != null ? "User created successfully" : "User creation failed",
            Data = mappedData
        };
    }

    public async Task<ResponseModel<UserDto?>> Login(string email, string password)
    {
        var user = await _userRepository.GetUserByEmail(email);

        if (user == null)
        {
            return new ResponseModel<UserDto?>
            {
                IsSuccess = false,
                Message = "User not found",
                Data = null
            };
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
        if (!isPasswordValid)
        {
            return new ResponseModel<UserDto?>
            {
                IsSuccess = false,
                Message = "Invalid username or password",
                Data = null
            };
        }

        user.LastLogin = DateTime.Now;
        await _userRepository.UpdateUserAsync(user);

        var mappedUser = _mapper.Map<UserDto>(user);
        FormatUserDetails(mappedUser);

        return new ResponseModel<UserDto?>
        {
            IsSuccess = true,
            Message = "Login successful",
            Data = mappedUser
        };
    }

    #endregion

    #region Private
    private static string GetIpAddress()
    {
        var context = new HttpContextAccessor().HttpContext;
        return context?.Connection?.RemoteIpAddress?.ToString() ?? "";
    }

    private static void FormatUserDetails(UserDto userDto)
    {
        userDto.DiplayLastLogin = FormatDate(userDto.LastLogin);
        userDto.BithDate = $"{userDto.BirthYear}-{userDto.BirthMonth}-{userDto.BirthDay}";

        userDto.Folders?.ForEach(folder =>
        {
            folder.DisplayDateCreated = FormatDate(folder.DateCreated);
            folder.DisplayLastModified = FormatDate(folder.LastModified);

            folder.Emails?.ForEach(email =>
            {
                email.DisplayDateRead = FormatDate(email.DateRead);
                email.DisplayDateReceived = FormatDate(email.DateReceived);
                email.DisplayDateSent = FormatDate(email.DateSent);

                email.Recipients?.ForEach(recipient =>
                {
                    recipient.DisplayDateRead = FormatDate(recipient.DateRead);
                    recipient.DisplayDateReceived = FormatDate(recipient.DateReceived);
                });
            });
        });

        userDto.Contacts?.ForEach(contact =>
        {
            contact.DisplayDateAdded = FormatDate(contact.DateAdded);
        });
    }

    private static string FormatDate(DateTime? date)
    {
        return date?.ToString("MMMM dd, yyyy hh:mmtt") ?? "";
    }

    private static void SetDefaultSettings(UserDto userDto, User userEntity)
    {
        userEntity.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        userEntity.CreatedAt = DateTime.Now;
        userEntity.IsActive = true;
        userEntity.CreatedIp = GetIpAddress();
        userEntity.LastLogin = DateTime.Now;

        if (userEntity.Contacts != null && userEntity.Contacts.Any())
        {
            foreach (var contact in userEntity.Contacts)
            {
                contact.DateAdded = DateTime.Now;
                contact.IsActive = true;
            }
        }

        if (userEntity.Folders != null && userEntity.Folders.Any())
        {
            foreach (var folder in userEntity.Folders)
            {
                folder.DateCreated = DateTime.Now;
                folder.IsDefault = IsDefaultFolder(folder.Name);
                folder.IsActive = true;
            }
        }
    }

    private static bool IsDefaultFolder(string folderName)
    {
        var defaultFolders = new List<string> { "Inbox", "Documents", "Photo" };
        return defaultFolders.Exists(name => name.Equals(folderName, StringComparison.OrdinalIgnoreCase));
    }

    #endregion
}
