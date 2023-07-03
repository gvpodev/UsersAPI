using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;

namespace UsersAPI.Application.Interfaces.Application
{
    public interface IAuthAppService : IDisposable
    {
        LoginResponseDto Login(LoginRequestDto request);
        UserResponseDto ForgotPassword(ForgotPasswordRequestDto request);
        UserResponseDto ResetPassword(ResetPasswordRequestDto request);
    }
}
