using eCommerce.Bussiness.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    public class CartsController : BaseController
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult GetAll()
        {
            return Ok(_cartService.GetAllCarts());
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _cartService.GetCartById(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost]
        public IActionResult Add()
        {
            var result = _cartService.AddCart(new DataAccess.Entities.Cart());
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _cartService.DeleteCartById(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
