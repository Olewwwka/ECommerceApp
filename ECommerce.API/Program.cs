using ECommerce.API.Endpoints;
using ECommerce.Application.Mappers;
using ECommerce.Application.Services;
using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Abstractions.ServicesInterfaces;
using ECommerce.Infrastructure.Caching;
using ECommerce.Infrastructure.Identity.Services;
using ECommerce.Infrastructure.Persistence.Configuration;
using ECommerce.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

services.AddSingleton<IRefreshTokenRepository, RefreshTokenRepository>();

services.AddDbContext<ECommerceDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(ECommerceDbContext)));
});

services.AddScoped<IUnitOfWork, UnitOfWork>();

services.AddScoped<UserService>();

services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

services.AddHttpContextAccessor();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapUsersEndpoints();
app.UseHttpsRedirection();
app.UseAuthorization();


app.Run();
