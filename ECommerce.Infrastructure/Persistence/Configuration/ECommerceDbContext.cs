using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ECommerce.Core.Entities;

namespace ECommerce.Infrastructure.Persistence.Configuration
{
    public class ECommerceDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ShoppingCartEntity> ShoppingCarts { get; set; }
        public DbSet<AuditLogsEntity> AuditLogs { get; set; }
        public ECommerceDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(userBuilder =>
            {
                userBuilder.ToTable("Users").HasKey(x => x.UserId);

                userBuilder.HasMany<OrderEntity>()
                    .WithOne()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                userBuilder.HasMany<ReviewEntity>()
                    .WithOne()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                userBuilder.HasMany<AuditLogsEntity>()
                    .WithOne(log => log.User)
                    .HasForeignKey(log => log.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                userBuilder.HasOne(user => user.ShoppingCart)
                    .WithOne(cart => cart.User)
                    .HasForeignKey<ShoppingCartEntity>(cart => cart.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ShoppingCartEntity>(cartBuilder =>
            {
                cartBuilder.ToTable("ShoppingCarts").HasKey(sc => sc.CartId);

                cartBuilder.HasOne(sc => sc.User)
                           .WithOne()
                           .HasForeignKey<ShoppingCartEntity>(sc => sc.UserId);
            });

            modelBuilder.Entity<OrderEntity>(orderBuilder =>
            {
                orderBuilder.ToTable("Orders").HasKey(o => o.UserId);

                orderBuilder.HasOne<UserEntity>()
                            .WithMany()
                            .HasForeignKey(o => o.UserId)
                            .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AuditLogsEntity>(logBuilder =>
            {
                logBuilder.ToTable("AuditLogs").HasKey(log => log.LogId);

                logBuilder.HasOne(log => log.User)
                          .WithMany()
                          .HasForeignKey(log => log.UserId)
                          .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ReviewEntity>(reviewBuilder =>
            {
                reviewBuilder.ToTable("Reviews").HasKey(r => r.ReviewId);

                reviewBuilder.HasOne(r => r.User)
                             .WithMany()
                             .HasForeignKey(r => r.UserId)
                             .OnDelete(DeleteBehavior.Cascade);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("ECommerceDb"))
                .UseLoggerFactory(CreateLoggerFactory())
                .EnableSensitiveDataLogging();
        }

        public ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { });
    }
}
