using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace UsersAPI.Api.Extensions
{
    public static class JwtBearerExtension
    {
        public static IServiceCollection AddJwtBearer(this IServiceCollection services)
        {
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
                    ValidateIssuerSigningKey = true // chave secreta utilizada pelo emissor do token
                };
            });

            return services;
        }
    }
}
