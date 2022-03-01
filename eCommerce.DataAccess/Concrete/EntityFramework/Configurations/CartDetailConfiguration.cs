using eCommerce.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.Concrete.EntityFramework.Configurations
{
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(e => new { e.CartId, e.ProductId });

            builder
                .HasOne(e => e.Cart)
                .WithMany(e => e.CartDetails)
                .HasForeignKey(e => e.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.Product)
                .WithMany(e => e.CartDetails)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.Quantity).HasPrecision(18, 2);
        }
    }
}
