using eCommerce.Core.DataAccess;
using eCommerce.DataAccess.Entities;

namespace eCommerce.DataAccess.Abstract
{
    public interface ICartDal : IReadOnlyEntityRepository<Cart>, ICRUDEntityRepository<Cart>
    {
    }
}
