using AutoMapper;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;
using UsersAPI.Domain.Exceptions;
using UsersAPI.Domain.Interfaces.Services;

namespace UsersAPI.Application.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IUserDomainService? _userDomainService;
        private readonly IMapper? _mapper;

        public AuthAppService(IUserDomainService userDomainService, IMapper? mapper)
        {
            _userDomainService = userDomainService;
            _mapper = mapper;
        }

        public UserResponseDto ForgotPassword(ForgotPasswordRequestDto request)
        {
            var user = _userDomainService?.Find(request.Email);

            return _mapper.Map<UserResponseDto>(user);
        }

        public LoginResponseDto Login(LoginRequestDto request)
        {
            try
            {
                var accessToken = _userDomainService?.Authenticate(request.Email, request.Password);

                return new LoginResponseDto { AccessToken = accessToken };
            }
            catch (AccessDeniedException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public UserResponseDto ResetPassword(Guid id, ResetPasswordRequestDto request)
        {
            var user = _userDomainService?.Find(id);

            return _mapper.Map<UserResponseDto>(user);
        }

        public void Dispose() => _userDomainService.Dispose();
    }
}
