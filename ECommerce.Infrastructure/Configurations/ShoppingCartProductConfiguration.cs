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
    public class ShoppingCartProductConfiguration : IEntityTypeConfiguration<ShoppingCartProductEntity>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartProductEntity> builder)
        {
            builder.HasKey(x => new { x.ShoppingCartId, x.ProductId });

            builder.HasOne(x => x.ShoppingCart)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
