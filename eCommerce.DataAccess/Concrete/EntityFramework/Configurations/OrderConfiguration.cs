using eCommerce.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.Concrete.EntityFramework.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Ignore(i => i.TotalPrice);
            builder.Property(e => e.CreatedDateTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");
        }
    }
}
