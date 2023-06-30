using Microsoft.OpenApi.Models;
using System.Reflection;

namespace UsersAPI.Api.Extensions
{
    public static class SwaggerDocExtension
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "UsersAPI",
                    Description = "API to users control",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Gabriel Vasconcellos",
                        Email = "gavapim@gmail.com",
                        Url = new Uri("https://github.com/gvpodev")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "UsersAPI");
            });

            return app;
        }
    }
}
