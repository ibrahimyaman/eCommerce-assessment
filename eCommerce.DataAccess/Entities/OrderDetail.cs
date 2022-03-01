using eCommerce.Core.DataAccess;

namespace eCommerce.DataAccess.Entities
{
    public class OrderDetail : IEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
    }
}
