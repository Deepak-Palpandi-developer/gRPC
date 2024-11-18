using AutoMapper;
using Gmail.Application.Services.UserServices;
using Gmail.Helpers.Exceptions;
using Gmail.User;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using UserModel;

namespace Gmail.Grpc.Api.Services.User
{
    public class UserService : UserProto.UserProtoBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUserService userService, ILogger<UserService> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<ResponseModel> GetUser(GetUserRequest request, ServerCallContext context)
        {
            try
            {
                var response = await _userService.GetUser(request.UserId);

                return new ResponseModel
                {
                    IsSuccess = response.IsSuccess,
                    Message = response.Message,
                    Token = response.Token,
                    User = _mapper.Map<UserDto>(response.Data)
                };
            }
            catch (BadRequestException brex)
            {
                _logger.LogWarning(brex, brex.Message);
                throw new RpcException(new Status(StatusCode.InvalidArgument, brex.Message));
            }
            catch (NotFoundException nfx)
            {
                _logger.LogWarning(nfx, nfx.Message);
                throw new RpcException(new Status(StatusCode.NotFound, $"{nfx.Source}: {nfx.Message}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }

        public override async Task<ResponseModel> GetUsers(Empty request, ServerCallContext context)
        {
            try
            {
                var response = await _userService.GetUser();

                var userDtos = response.Data != null && response.Data.Any() ?
                    response.Data.Select(user => _mapper.Map<UserDto>(user)).ToList() : null;

                return new ResponseModel
                {
                    IsSuccess = response.IsSuccess,
                    Message = response.Message,
                    Token = response.Token,
                    Users = { userDtos }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }

        public override async Task<ResponseModel> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            try
            {
                var userDto = _mapper.Map<Gmail.Application.Dto.User.UserDto>(request.User);

                var response = await _userService.CreateUser(userDto);
                return new ResponseModel
                {
                    IsSuccess = response.IsSuccess,
                    Message = response.Message,
                    Token = response.Token,
                    User = _mapper.Map<UserDto>(response.Data)
                };
            }
            catch (BadRequestException brex)
            {
                _logger.LogWarning(brex, brex.Message);
                throw new RpcException(new Status(StatusCode.InvalidArgument, brex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }

        public override async Task<ResponseModel> LoginUser(LoginRequest request, ServerCallContext context)
        {
            try
            {
                var response = await _userService.Login(request.Email, request.Password);

                return new ResponseModel
                {
                    IsSuccess = response.IsSuccess,
                    Message = response.Message,
                    Token = response.Token,
                    User = _mapper.Map<UserDto>(response.Data)
                };
            }
            catch (BadRequestException brex)
            {
                _logger.LogWarning(brex, brex.Message);
                throw new RpcException(new Status(StatusCode.InvalidArgument, brex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }
    }
}
