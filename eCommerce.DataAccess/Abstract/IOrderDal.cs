using eCommerce.Core.DataAccess;
using eCommerce.DataAccess.Entities;

namespace eCommerce.DataAccess.Abstract
{
    public interface IOrderDal : IReadOnlyEntityRepository<Order>, ICRUDEntityRepository<Order>
    {
        Order PrepareOrderByCartId(int cartId);
    }
}
