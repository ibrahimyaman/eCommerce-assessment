using eCommerce.Bussiness.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public OrdersController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        public IActionResult GetAll()
        {
            return Ok(_orderService.GetAllOrders());
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _orderService.GetOrderById(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }
        [HttpGet("{id:int}/orderdetails")]
        public IActionResult GetAllOderDetails(int id)
        {
            return Ok(_orderService.GetAllOrderDetailsByOrderId(id));
        }
        [HttpGet("{id:int}/orderdetails/{productId:int}")]
        public IActionResult GetOrderDetailById(int id, int productId)
        {
            var result = _orderService.GetOrderDetailByOrderAndProductId(id, productId);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }


        [HttpGet("createorderfromcart/{cartId:int}")]
        public IActionResult CreateOrderFromCart(int cartId)
        {
            var result = _cartService.MakeOrder(cartId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
