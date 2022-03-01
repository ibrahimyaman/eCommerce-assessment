using eCommerce.Core.Utilities.Results;
using eCommerce.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eCommerce.Bussiness.Abstract
{
    public interface IOrderService
    {
        IDataResult<IEnumerable<Order>> GetAllOrders(Expression<Func<Order, bool>> filter = null);
        IDataResult<Order> GetOrderById(int id);

        IDataResult<IEnumerable<OrderDetail>> GetAllOrderDetailsByOrderId(int orderId);
        IDataResult<OrderDetail> GetOrderDetailByOrderAndProductId(int orderId, int productId);
    }
}
