using eCommerce.API.Utilities.Extentions;
using eCommerce.Bussiness.Abstract;
using eCommerce.DataAccess.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult GetAll()
        {
            return Ok(_productService.GetAllProducts());
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetProductById(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost]
        public IActionResult Add(ProductAddDto productAddDto)
        {
            if (!ModelState.IsValid)
                return NotValid(productAddDto);

            var result = _productService.AddProduct(productAddDto.ToProduct());
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, ProductUpdateDto productUpdateDto)
        {
            if (!ModelState.IsValid)
                return NotValid(productUpdateDto);

            productUpdateDto.Id = id;
            var result = _productService.UpdateProduct(productUpdateDto.ToProduct());
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.DeleteProductById(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
