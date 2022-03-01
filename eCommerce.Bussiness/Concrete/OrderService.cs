using eCommerce.Bussiness.Abstract;
using eCommerce.Core.Utilities.Results;
using eCommerce.DataAccess.Abstract;
using eCommerce.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eCommerce.Bussiness.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IOrderDetailDal _orderDetailDal;

        public OrderService(IOrderDal orderDal, IOrderDetailDal orderDetailDal)
        {
            _orderDal = orderDal;
            _orderDetailDal = orderDetailDal;
        }

        public IDataResult<IEnumerable<Order>> GetAllOrders(Expression<Func<Order, bool>> filter = null)
        {
            return new SuccessDataResult<IEnumerable<Order>>(_orderDal.GetList(filter, i => i.OrderDetails));
        }

        public IDataResult<Order> GetOrderById(int id)
        {
            var order = _orderDal.Get(w => w.Id.Equals(id), i => i.OrderDetails);
            if (order is null)
                return new ErrorDataResult<Order>("Record not found");

            return new SuccessDataResult<Order>(order);
        }
        public IDataResult<IEnumerable<OrderDetail>> GetAllOrderDetailsByOrderId(int orderId)
        {
            return new SuccessDataResult<IEnumerable<OrderDetail>>(_orderDetailDal.GetList(w => w.OrderId.Equals(orderId), i => i.Product));
        }
        public IDataResult<OrderDetail> GetOrderDetailByOrderAndProductId(int orderId, int productId)
        {
            var orderDetail = _orderDetailDal.Get(w => w.OrderId.Equals(orderId) && w.ProductId.Equals(productId), i => i.Product);
            if (orderDetail is null)
                return new ErrorDataResult<OrderDetail>("Record not found");

            return new SuccessDataResult<OrderDetail>(orderDetail);
        }
    }
}
