using eCommerce.Bussiness.Abstract;
using eCommerce.Core.Utilities.Results;
using eCommerce.DataAccess.Abstract;
using eCommerce.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace eCommerce.Bussiness.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartDal _cartDal;
        private readonly IOrderDal _orderDal;
        private readonly IProductDal _productDal;
        private readonly ICartDetailDal _cartDetailDal;

        public CartService(ICartDal cartDal, ICartDetailDal cartDetailDal, IProductDal productDal, IOrderDal orderDal)
        {
            _cartDal = cartDal;
            _cartDetailDal = cartDetailDal;
            _productDal = productDal;
            _orderDal = orderDal;
        }

        public IDataResult<Cart> AddCart(Cart cart)
        {
            _cartDal.Add(cart);

            return new SuccessDataResult<Cart>(cart, "new record added");
        }

        public IDataResult<Cart> DeleteCartById(int id)
        {
            var cartResult = IsExistCartById(id);
            if (!cartResult.Success)
                return cartResult;

            var cart = cartResult.Data;

            if (cart.IsOrdered)
                return new ErrorDataResult<Cart>("this cart turned ordered");

            _cartDal.Delete(cart);

            return new SuccessDataResult<Cart>(cart, "Record deleted");
        }

        public IDataResult<IEnumerable<Cart>> GetAllCarts(Expression<Func<Cart, bool>> filter = null)
        {
            return new SuccessDataResult<IEnumerable<Cart>>(_cartDal.GetList(filter, i => i.CartDetails));
        }

        public IDataResult<Cart> GetCartById(int id)
        {
            var cart = _cartDal.Get(w => w.Id.Equals(id), i => i.CartDetails);
            if (cart is null)
                return new ErrorDataResult<Cart>("Record not found");
            var productIds = cart.CartDetails?.Select(s => s.ProductId)?.ToArray() ?? new int[0];
            var products = _productDal.GetList(w => productIds.Contains(w.Id));

            cart.CartDetails.ToList()
                .ForEach(f =>
                {
                    f.Product = products.SingleOrDefault(w => w.Id.Equals(f.ProductId));
                });

            return new SuccessDataResult<Cart>(cart);
        }

        public IDataResult<Cart> AddCartDetail(CartDetail cartDetail)
        {
            var cartResult = IsExistCartById(cartDetail.CartId);
            if (!cartResult.Success)
                return cartResult;

            if (cartResult.Data.IsOrdered)
                return new ErrorDataResult<Cart>("this cart turned ordered");

            var productResult = IsExistProductById(cartDetail.ProductId);
            if (!productResult.Success)
                return new ErrorDataResult<Cart>(cartResult.Data, productResult.Message);

            var oldCartDetail = _cartDetailDal.Get(w => w.CartId.Equals(cartDetail.CartId) && w.ProductId.Equals(cartDetail.ProductId));
            if (oldCartDetail is null)
            {
                if (productResult.Data.StockQuantity < cartDetail.Quantity)
                    return new ErrorDataResult<Cart>(cartResult.Data, "Quantity more than available stock");

                _cartDetailDal.Add(cartDetail);
            }
            else
            {
                return UpdateCartDetail(cartDetail);
            }
            cartResult = GetCartById(cartDetail.CartId);
            return new SuccessDataResult<Cart>(cartResult.Data, "New cart item added");
        }

        public IDataResult<Cart> UpdateCartDetail(CartDetail cartDetail)
        {
            var cartResult = IsExistCartById(cartDetail.CartId);
            if (!cartResult.Success)
                return cartResult;

            if (cartResult.Data.IsOrdered)
                return new ErrorDataResult<Cart>("this cart turned ordered");

            var productResult = IsExistProductById(cartDetail.ProductId);
            if (!productResult.Success)
                return new ErrorDataResult<Cart>(cartResult.Data, productResult.Message);

            var oldCartDetail = _cartDetailDal.Get(w => w.CartId.Equals(cartDetail.CartId) && w.ProductId.Equals(cartDetail.ProductId));
            if (oldCartDetail is null)
                return new ErrorDataResult<Cart>(cartResult.Data, "Cart item not found");
            else
            {
                oldCartDetail.Quantity = cartDetail.Quantity;

                if (productResult.Data.StockQuantity < oldCartDetail.Quantity)
                    return new ErrorDataResult<Cart>(cartResult.Data, "Quantity more than available stock");

                _cartDetailDal.Update(oldCartDetail);
            }
            cartResult = GetCartById(cartDetail.CartId);
            return new SuccessDataResult<Cart>(cartResult.Data, "Cart item updated");
        }

        public IDataResult<Cart> DeleteCartDetailByCartAndProductId(int cartId, int productId)
        {
            var cartResult = IsExistCartById(cartId);
            if (!cartResult.Success)
                return cartResult;

            if (cartResult.Data.IsOrdered)
                return new ErrorDataResult<Cart>("this cart turned ordered");

            var productResult = IsExistProductById(productId);
            if (!productResult.Success)
                return new ErrorDataResult<Cart>(cartResult.Data, productResult.Message);

            var oldCartDetail = _cartDetailDal.Get(w => w.CartId.Equals(cartId) && w.ProductId.Equals(productId));
            if (oldCartDetail is null)
                return new ErrorDataResult<Cart>(cartResult.Data, "Cart item not found");
            else
            {                
                _cartDetailDal.Delete(oldCartDetail);
            }

            cartResult = GetCartById(cartId);
            return new SuccessDataResult<Cart>(cartResult.Data, "Cart item deleted");
        }

        private IDataResult<Cart> IsExistCartById(int id)
        {
            var cart = _cartDal.Get(w => w.Id.Equals(id));
            if (cart is null)
                return new ErrorDataResult<Cart>("Cart not found");

            return new SuccessDataResult<Cart>(cart);
        }

        private IDataResult<Product> IsExistProductById(int id)
        {
            var product = _productDal.Get(w => w.Id.Equals(id));
            if (product is null)
                return new ErrorDataResult<Product>("Product not found");

            return new SuccessDataResult<Product>(product);
        }

        public IDataResult<Order> MakeOrder(int cartId)
        {
            var cartResult = IsExistCartById(cartId);
            if (!cartResult.Success)
                return new ErrorDataResult<Order>(null, cartResult.Message);

            if (cartResult.Data.IsOrdered)
                return new ErrorDataResult<Order>("this cart turned ordered before");

            var order = _orderDal.PrepareOrderByCartId(cartId);
            if(order is null)
            {
                cartResult = GetCartById(cartId);
                var cart = cartResult.Data;
                var orderDetails = cart.CartDetails.Where(w => w.Product.StockQuantity <= 0).ToArray();
                _cartDetailDal.DeleteRange(orderDetails);

                return new ErrorDataResult<Order>(order, "Order cannot be created. Check quantities of products in cart");
            }

            return new SuccessDataResult<Order>(order, "Order has been created");
        }

        public IDataResult<IEnumerable<CartDetail>> GetAllCartDetailsByCartId(int cartId)
        {
            return new SuccessDataResult<IEnumerable<CartDetail>>(_cartDetailDal.GetList(w => w.CartId.Equals(cartId)));        
        }

        public IDataResult<CartDetail> GetCartDetailByCartAndProductId(int cartId, int productId)
        {
            var cartDetail = _cartDetailDal.Get(w => w.CartId.Equals(cartId) && w.ProductId.Equals(productId));
            if (cartDetail is null)
                return new ErrorDataResult<CartDetail>("Record not found");

            return new SuccessDataResult<CartDetail>(cartDetail);
        }
    }
}
