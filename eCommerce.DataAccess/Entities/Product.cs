using eCommerce.Core.DataAccess;
using System;
using System.Collections.Generic;

namespace eCommerce.DataAccess.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal StockQuantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
