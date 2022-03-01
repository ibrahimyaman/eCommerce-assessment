using eCommerce.Core.DataAccess;
using eCommerce.DataAccess.Entities;

namespace eCommerce.DataAccess.Abstract
{
    public interface ICartDetailDal : IReadOnlyEntityRepository<CartDetail>, ICRUDEntityRepository<CartDetail>
    {
    }
}
