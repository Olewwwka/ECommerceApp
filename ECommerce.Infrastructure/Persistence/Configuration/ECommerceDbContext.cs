using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ECommerce.Core.Entities;
using Microsoft.Extensions.Options;
using ECommerce.Infrastructure.Configurations;

namespace ECommerce.Infrastructure.Persistence.Configuration
{
    public class ECommerceDbContext(DbContextOptions<ECommerceDbContext> options,
        IOptions<AuthorizationOptions> authOptions) : DbContext(options)
    {
        private readonly IConfiguration _configuration;
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ShoppingCartEntity> ShoppingCarts { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDbContext).Assembly);

            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions.Value));
        }
    }
}
