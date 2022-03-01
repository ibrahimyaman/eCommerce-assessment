using AutoMapper;
using eCommerce.API.Utilities;
using eCommerce.API.Utilities.Mapper.AutoMapperProfiles;
using eCommerce.Bussiness.DependencyResolvers.Microsoft;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace eCommerce.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddCustomServices();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductProfile());
                mc.AddProfile(new CartProfile());
            });

            services.AddSingleton(mapperConfig.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StaticServiceProvider.Provider = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
