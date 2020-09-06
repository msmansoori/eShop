using eShop.InternalServer.Interfaces;
using eShop.InternalServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.InternalServer.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddCoreConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IAudience, Audience>();
            services.AddTransient<IPersistence, Persistence>();
            services.AddTransient<IDatabase, Database>();
            return services;
        }

        public static IApplicationBuilder AddCoreConfigure(this IApplicationBuilder app)
        {
            return app;
        }
    }
}