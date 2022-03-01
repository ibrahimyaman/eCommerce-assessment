using System;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.DataAccess.Dtos
{
    public class CartDetailAddDto
    {
        public int CartId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Quantity { get; set; }
    }
}
