using ECommerce.Core.Entities;
using ECommerce.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> permissionBuilder)
        {
            permissionBuilder.ToTable("Permissions").HasKey(x => x.Id);

            var permissions = Enum
                .GetValues<Permission>()
                .Select(p => new PermissionEntity
                {
                    Id = (int)p,
                    Name = p.ToString()
                });

            permissionBuilder.HasData(permissions);
        }
    }
}
