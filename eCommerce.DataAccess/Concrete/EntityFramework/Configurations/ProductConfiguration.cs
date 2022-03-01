using eCommerce.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.Concrete.EntityFramework.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(250).IsRequired();
            builder.Property(e => e.Brand).HasMaxLength(75).IsRequired();
            builder.Property(e => e.StockQuantity).HasPrecision(18,2);
            builder.Property(e => e.Price).HasPrecision(18,4);
            builder.Property(e => e.CreatedDateTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");
            builder.Property(e => e.ModifiedDateTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");
        }
    }
}
