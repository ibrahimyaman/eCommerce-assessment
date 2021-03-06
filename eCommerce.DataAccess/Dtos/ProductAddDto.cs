using eCommerce.Core.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.DataAccess.Dtos
{
    public class ProductAddDto : IDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [Required]
        [MaxLength(75)]
        public string Brand { get; set; }
        public decimal StockQuantity { get; set; }
        public decimal Price { get; set; }
    }
}
