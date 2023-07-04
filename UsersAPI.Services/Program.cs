using UsersAPI.Api.Extensions;
using UsersAPI.Infra.IoC.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerDoc();
builder.Services.AddJwtBearer();
builder.Services.AddCorsPolicy();
builder.Services.AddAutoMapperConfig();
builder.Services.AddDependencyInjection();

var app = builder.Build();

app.UseSwaggerDoc();
app.UseCorsPolicy();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
