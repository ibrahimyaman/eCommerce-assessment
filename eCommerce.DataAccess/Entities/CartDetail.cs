using eCommerce.Core.DataAccess;

namespace eCommerce.DataAccess.Entities
{
    public class CartDetail : IEntity
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
    }
}
