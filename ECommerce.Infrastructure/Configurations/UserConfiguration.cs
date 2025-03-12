using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> userBuilder)
        {
            userBuilder.ToTable("Users").HasKey(x => x.UserId);

            userBuilder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<UserRoleEntity>(
                    l => l.HasOne<RoleEntity>().WithMany().HasForeignKey(x => x.RoleId),
                    r => r.HasOne<UserEntity>().WithMany().HasForeignKey(x => x.UserId)
                );

            userBuilder.HasOne(x => x.ShoppingCart)
                .WithOne(x => x.User)
                .HasForeignKey<ShoppingCartEntity>(x => x.UserId);
        }
    }
}
