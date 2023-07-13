using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using UsersAPI.Domain.Interfaces.Secutiry;
using UsersAPI.Domain.ValueObjects;
using UsersAPI.Infra.Security.Settings;

namespace UsersAPI.Infra.Security.Services;

public class TokenService : ITokenService
{
    private readonly TokenSettings? _tokenSettings;

    public TokenService(IOptions<TokenSettings> tokenSettings)
    {
        _tokenSettings = tokenSettings.Value;
    }

    public string CreateToken(UserAuthVO userAuthVo)
    {
        // Definir as CLAIMS que serão gravadas no token
        // CLAIMS -> Identificações para o usuário
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(userAuthVo)),
            new Claim(ClaimTypes.Role, userAuthVo.Role)
        };

        // Gerando assinatura antifalsificação 
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings?.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        // Informações do Token
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _tokenSettings?.Issuer,
            audience: _tokenSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_tokenSettings?.ExpirationInMinutes)),
            signingCredentials: credentials);

        // Retornando o token
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(jwtSecurityToken);
    }
}