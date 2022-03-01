using eCommerce.Core.DataAccess;
using eCommerce.DataAccess.Entities;

namespace eCommerce.DataAccess.Abstract
{
    public interface IOrderDetailDal : IReadOnlyEntityRepository<OrderDetail>, ICRUDEntityRepository<OrderDetail>
    {
    }
}
