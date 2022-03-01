using eCommerce.Bussiness.Abstract;
using eCommerce.Bussiness.Concrete;
using eCommerce.DataAccess.Abstract;
using eCommerce.DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Bussiness.DependencyResolvers.Microsoft
{
    public static class MicrosoftDI
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartService, CartService>();

            services.AddScoped<IProductDal, EfProductDal>();
            services.AddScoped<ICartDal, EfCartDal>();
            services.AddScoped<ICartDetailDal, EfCartDetailDal>();
            services.AddScoped<IOrderDal, EfOrderDal>();
            services.AddScoped<IOrderDetailDal, EfOrderDetailDal>();

            return services;
        }
    }
}
