using ECommerce.Core.Entities;
using ECommerce.Core.Enums;
using ECommerce.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
    {
        private readonly AuthorizationOptions _authOptions;
        public RolePermissionConfiguration(AuthorizationOptions authOptions)
        {
            _authOptions = authOptions;
        }
        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.ToTable("RolePermission").HasKey(x => new { x.RoleId, x.PermissionId });
           
            builder.HasData(ParseRolePermissions());
        }
        private List<RolePermissionEntity> ParseRolePermissions()
        {
            return _authOptions.RolePermissions
                .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermissionEntity
                {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permission>(p)
                })).ToList();
        }
    }
}
