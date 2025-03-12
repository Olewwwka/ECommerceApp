using ECommerce.API.Endpoints;
using ECommerce.API.Extensions;
using ECommerce.Application.Mappers;
using ECommerce.Application.Services;
using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Abstractions.ServicesInterfaces;
using ECommerce.Infrastructure.Caching;
using ECommerce.Infrastructure.Identity.Services;
using ECommerce.Infrastructure.Persistence.Configuration;
using ECommerce.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));

services.AddSingleton<IRefreshTokenRepository, RefreshTokenRepository>();

services.AddDbContext<ECommerceDbContext>((serviceProvider, options) =>
{
    var config = serviceProvider.GetRequiredService<IConfiguration>();
    var authOptions = serviceProvider.GetRequiredService<IOptions<AuthorizationOptions>>();

    options.UseNpgsql(config.GetConnectionString(nameof(ECommerceDbContext)));

    options.UseApplicationServiceProvider(serviceProvider);
});

services.AddApiAutorization(configuration);

services.AddHttpContextAccessor();

services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<UserService>();

services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddMappedEndpoints();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("get", () =>
{
    return Results.Ok("ok");
}).RequireAuthorization("AdminPolicy");


app.Run();
