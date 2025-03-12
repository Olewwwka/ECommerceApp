using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> productBuilder)
        {
            productBuilder.ToTable("Products").HasKey(x => x.ProductId);

            productBuilder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
