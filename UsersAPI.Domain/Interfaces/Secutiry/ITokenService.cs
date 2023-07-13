using UsersAPI.Domain.ValueObjects;

namespace UsersAPI.Domain.Interfaces.Secutiry;

public interface ITokenService
{
    string CreateToken(UserAuthVO userAuthVo);
}