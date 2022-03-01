using eCommerce.Core.Utilities.Results;
using eCommerce.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eCommerce.Bussiness.Abstract
{
    public interface IProductService
    {
        IDataResult<IEnumerable<Product>> GetAllProducts(Expression<Func<Product, bool>> filter = null);
        IDataResult<Product> GetProductById(int id);
        IDataResult<Product> AddProduct(Product product);
        IDataResult<Product> UpdateProduct(Product product);
        IDataResult<Product> DeleteProductById(int id);
    }
}
