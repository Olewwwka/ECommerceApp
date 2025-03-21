using ECommerce.API.Extensions;
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

services.AddDbContext<ECommerceDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(ECommerceDbContext)));
});
services.AddApiAutorization(configuration,
    builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());

services.AddHttpContextAccessor();

services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<UserService>();
services.AddScoped<CategoryService>();
services.AddScoped<ProductsService>();
services.AddScoped<UserCartService>();

services.AddScoped<ICurrentUserService, CurrentUserService>();
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



app.Run();
    