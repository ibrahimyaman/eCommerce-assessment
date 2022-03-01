using eCommerce.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.DataAccess.Concrete.EntityFramework.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(e => e.CreatedDateTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");
        }
    }
}
