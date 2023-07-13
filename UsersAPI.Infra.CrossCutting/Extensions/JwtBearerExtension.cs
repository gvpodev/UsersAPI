using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UsersAPI.Domain.Interfaces.Secutiry;
using UsersAPI.Infra.Security.Services;
using UsersAPI.Infra.Security.Settings;

namespace UsersAPI.Infra.IoC.Extensions
{
    public static class JwtBearerExtension
    {
        public static IServiceCollection AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            // Ler as configs do appsettings
            var issuer = configuration.GetSection("TokenSettings:Issuer").Value;
            var audience = configuration.GetSection("TokenSettings:Audience").Value;
            var secretKey = configuration.GetSection("TokenSettings:SecretKey").Value;
            var expirationInMinutes = int.Parse(configuration.GetSection("TokenSettings:ExpirationInMinutes").Value);
            
            // definindo a política de autenticação do projeto
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // definindo as preferências para autenticação com JWT
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // emissor
                    ValidateAudience = true, // destinatário
                    ValidateLifetime = true, // tempo de expiração
                    ValidateIssuerSigningKey = true, // chave secreta utilizada pelo emissor do token
                    
                    ValidIssuer = issuer, // nome do emissor
                    ValidAudience = audience, // cliente do token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) // chave antifalsificação
                };
            });

            services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));
            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
