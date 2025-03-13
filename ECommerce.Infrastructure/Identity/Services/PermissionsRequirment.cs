using ECommerce.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Infrastructure.Identity.Services
{
    public class PermissionsRequirment(Permission[] permissions) : IAuthorizationRequirement
    {
        public Permission[] Permissions { get; set; } = permissions;
    }
}
