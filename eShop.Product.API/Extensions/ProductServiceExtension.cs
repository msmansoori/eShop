using eShop.InternalServer.Extensions;
using eShop.InternalServer.Interfaces;
using eShop.Product.API.Logics;
using eShop.ProductEntities.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Product.API.Extensions
{
    public static class ProductServiceExtension
    {
        public static IServiceCollection AddProductConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSQLContext<ProductContext>(configuration: configuration);
            services.AddCoreConfigureServices();

            services.AddTransient<IMicroKernel, MicroKernel>();
            return services;
        }

        public static IApplicationBuilder AddProductConfigure(this IApplicationBuilder app)
        {
            app.InitDatabase<ProductContext>();
            return app;
        }
    }
}
