using eCommerce.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.DataAccess.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return OrderDetails?.Sum(s => s.Quantity * s.UnitPrice) ?? 0m;
            }
        }
        public DateTime CreatedDateTime { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
