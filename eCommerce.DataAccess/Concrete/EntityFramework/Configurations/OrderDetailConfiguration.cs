using eCommerce.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.Concrete.EntityFramework.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(e => new { e.OrderId, e.ProductId });

            builder
                .HasOne(e => e.Order)
                .WithMany(e => e.OrderDetails)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.Product)
                .WithMany(e => e.OrderDetails)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.UnitPrice).HasPrecision(18, 4);
            builder.Property(e => e.Quantity).HasPrecision(18, 2);
        }
    }
}
