using eCommerce.Core.DataAccess;
using eCommerce.DataAccess.Entities;

namespace eCommerce.DataAccess.Abstract
{
    public interface IProductDal : IReadOnlyEntityRepository<Product>, ICRUDEntityRepository<Product>
    {
    }
}
