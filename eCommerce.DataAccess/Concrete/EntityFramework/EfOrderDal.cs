using eCommerce.Core.DataAccess.EntityFramework;
using eCommerce.DataAccess.Abstract;
using eCommerce.DataAccess.Concrete.EntityFramework.Contexts;
using eCommerce.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eCommerce.DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, eCommerceDbContext>, IOrderDal
    {
        public Order PrepareOrderByCartId(int cartId)
        {
            using (var context = new eCommerceDbContext())
            {
                var cart = context.Cart.Include(i => i.CartDetails).ThenInclude(i => i.Product).SingleOrDefault(w => w.Id.Equals(cartId));

                using (var scope = context.Database.BeginTransaction())
                {
                    var orderDeetails = cart.CartDetails
                        .Select(s =>
                        {
                            var product = s.Product;
                            product.StockQuantity -= s.Quantity;                            

                            return new OrderDetail
                            {
                                ProductId = s.ProductId,
                                Quantity = s.Quantity,
                                UnitPrice = product.Price
                            };

                        }).ToList();

                    var order = new Order { OrderDetails = orderDeetails };

                    context.Order.Add(order);

                    cart.IsOrdered = true;

                    context.SaveChanges();

                    if (context.Product.Any(w => w.StockQuantity < 0))
                    {
                        scope.Rollback();
                        return default;
                    }
                    else
                        scope.Commit();

                    return order;
                }
            }
        }
    }
}
