using ECommerce.Core.Entities;
using ECommerce.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> roleBuilder)
        {
            roleBuilder.ToTable("Roles").HasKey(x => x.RoleId);

            roleBuilder.HasMany(x => x.Permisions)
                .WithMany(x => x.Roles)
                .UsingEntity<RolePermissionEntity>(
                    l => l.HasOne<PermissionEntity>().WithMany().HasForeignKey(x => x.PermissionId),
                    r => r.HasOne<RoleEntity>().WithMany().HasForeignKey(x => x.RoleId)
                );

            var roles = Enum
                .GetValues<Role>()
                .Select(r => new RoleEntity
                {
                    RoleId = (int)r,
                    RoleName = r.ToString()
                });

            roleBuilder.HasData(roles);

        }
    }
}
