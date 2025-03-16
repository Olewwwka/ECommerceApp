using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ECommerce.Infrastructure.Identity.Services;
using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Abstractions.ServicesInterfaces;
namespace ECommerce.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId =>
            int.TryParse(_httpContextAccessor.HttpContext?.User.FindFirst(CustomClaims.UserId)?.Value, out var userId)
            ? userId
            : null;

        public string? Email =>
            _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

        public List<string> Roles =>
            _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList() ?? new List<string>();
    }
}
