using eShop.Identity.API.Logics;
using eShop.IdentityEntities.Context;
using eShop.InternalServer.Extensions;
using eShop.InternalServer.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Identity.API.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSQLContext<IdentityContext>(configuration: configuration);
            services.AddCoreConfigureServices();

            services.AddTransient<IMicroKernel, MicroKernel>();
            return services;
        }

        public static IApplicationBuilder AddIdentityConfigure(this IApplicationBuilder app)
        {
            app.InitDatabase<IdentityContext>();
            return app;
        }
    }
}
