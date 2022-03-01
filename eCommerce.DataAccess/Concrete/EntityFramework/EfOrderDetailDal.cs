using eCommerce.Core.DataAccess.EntityFramework;
using eCommerce.DataAccess.Abstract;
using eCommerce.DataAccess.Concrete.EntityFramework.Contexts;
using eCommerce.DataAccess.Entities;

namespace eCommerce.DataAccess.Concrete.EntityFramework
{
    public class EfOrderDetailDal : EfEntityRepositoryBase<OrderDetail, eCommerceDbContext>, IOrderDetailDal
    {
    }
}
