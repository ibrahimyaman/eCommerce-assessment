using eCommerce.API.Utilities.Extentions;
using eCommerce.Bussiness.Abstract;
using eCommerce.DataAccess.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/carts/{id:int}/[controller]")]
    public class CartDetailsController : BaseController
    {
        private readonly ICartService _cartService;

        public CartDetailsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult GetAllCartdetail(int id)
        {
            return Ok(_cartService.GetAllCartDetailsByCartId(id));
        }
        [HttpGet("{productId:int}")]
        public IActionResult GetCartDetailById(int id, int productId)
        {
            var result = _cartService.GetCartDetailByCartAndProductId(id, productId);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost]
        public IActionResult Add(int id, CartDetailAddDto cartDetailAddDto)
        {
            if (!ModelState.IsValid)
                return NotValid(cartDetailAddDto);
            cartDetailAddDto.CartId = id;
            var result = _cartService.AddCartDetail(cartDetailAddDto.ToCartDetail());
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{productId:int}")]
        public IActionResult Update(int id, int productId, CartDetailUpdateDto cartDetailUpdateDto)
        {
            if (!ModelState.IsValid)
                return NotValid(cartDetailUpdateDto);

            cartDetailUpdateDto.CartId = id;
            cartDetailUpdateDto.ProductId = productId;
            var result = _cartService.UpdateCartDetail(cartDetailUpdateDto.ToCartDetail());
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
        [HttpDelete("{productId:int}")]
        public IActionResult Delete(int id, int productId)
        {
            var result = _cartService.DeleteCartDetailByCartAndProductId(id, productId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
