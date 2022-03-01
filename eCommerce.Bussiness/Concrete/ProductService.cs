using eCommerce.Bussiness.Abstract;
using eCommerce.Core.Utilities.Results;
using eCommerce.DataAccess.Abstract;
using eCommerce.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eCommerce.Bussiness.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductService(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<Product> AddProduct(Product product)
        {
            _productDal.Add(product);

            return new SuccessDataResult<Product>(product, "new item added");
        }
        public IDataResult<Product> UpdateProduct(Product product)
        {
            var oldProduct = _productDal.Get(w => w.Id.Equals(product.Id));
            if (oldProduct is null)
                return new ErrorDataResult<Product>("Record not found");

            oldProduct.Name = product.Name;
            oldProduct.Brand = product.Brand;
            oldProduct.StockQuantity = product.StockQuantity;
            oldProduct.Price = product.Price;
            oldProduct.ModifiedDateTime = DateTime.Now;

            _productDal.Update(oldProduct);

            return new SuccessDataResult<Product>(oldProduct);
        }
        public IDataResult<Product> DeleteProductById(int id)
        {
            var product = _productDal.Get(w => w.Id.Equals(id), i => i.CartDetails, i => i.OrderDetails);
            if (product is null)
                return new ErrorDataResult<Product>("Record not found");

            if (product.OrderDetails.Count > 0)
                return new ErrorDataResult<Product>(product, "There is an order with this product, thus it cannot be deleted");

            _productDal.Delete(product);
            return new SuccessDataResult<Product>(product, "Record deleted");
        }

        public IDataResult<IEnumerable<Product>> GetAllProducts(Expression<Func<Product, bool>> filter = null)
        {
            return new SuccessDataResult<IEnumerable<Product>>(_productDal.GetList(filter));
        }

        public IDataResult<Product> GetProductById(int id)
        {
            var product = _productDal.Get(w => w.Id.Equals(id));
            if (product is null)
                return new ErrorDataResult<Product>("Record not found");

            return new SuccessDataResult<Product>(product);
        }        
    }
}
