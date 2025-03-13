using Core.Abstractions.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Identity.Services
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionsRequirment>
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public PermissionAuthorizationHandler(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionsRequirment requirement)
        {
            var userId = context.User.Claims.FirstOrDefault(
                c => c.Type == CustomClaims.UserId);

            if (userId is null || !int.TryParse(userId.Value, out var id)) return;

            using var scope = _scopeFactory.CreateScope();

            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            var permissions = await permissionService.GetPermissionsAsync(id);

            if(permissions.Intersect(requirement.Permissions).Any())
            {
                context.Succeed(requirement);
            }
        }
    }
}
