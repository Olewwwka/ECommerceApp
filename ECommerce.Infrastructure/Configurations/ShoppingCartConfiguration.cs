using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCartEntity>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartEntity> builder)
        {
            builder.ToTable("ShoppingCarts").HasKey(x => x.CartId);

            builder.HasMany(x => x.Products)
                .WithOne(x => x.ShoppingCart)
                .HasForeignKey(x => x.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
