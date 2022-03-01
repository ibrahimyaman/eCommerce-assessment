using eCommerce.Core.Utilities.Results;
using eCommerce.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eCommerce.Bussiness.Abstract
{
    public interface ICartService
    {
        IDataResult<IEnumerable<Cart>> GetAllCarts(Expression<Func<Cart, bool>> filter = null);
        IDataResult<Cart> GetCartById(int id);
        IDataResult<Cart> AddCart(Cart cart);
        IDataResult<Cart> DeleteCartById(int id);
        IDataResult<IEnumerable<CartDetail>> GetAllCartDetailsByCartId(int cartId);
        IDataResult<CartDetail> GetCartDetailByCartAndProductId(int cartId, int productId);
        IDataResult<Cart> AddCartDetail(CartDetail cartDetail);
        IDataResult<Cart> UpdateCartDetail(CartDetail cartDetail);
        IDataResult<Cart> DeleteCartDetailByCartAndProductId(int cartId, int productId);
        IDataResult<Order> MakeOrder(int cartId);
    }
}
