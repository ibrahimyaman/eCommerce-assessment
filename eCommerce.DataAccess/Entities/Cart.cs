using eCommerce.Core.DataAccess;
using System;
using System.Collections.Generic;

namespace eCommerce.DataAccess.Entities
{
    public class Cart : IEntity
    {
        public int Id { get; set; }
        public bool IsOrdered { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
