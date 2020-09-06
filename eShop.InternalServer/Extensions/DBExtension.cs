using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.InternalServer.Extensions
{
    public static class DBExtension
    {
        public static IServiceCollection AddSQLContext<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IApplicationBuilder InitDatabase<TContext>(this IApplicationBuilder app) where TContext : DbContext
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TContext>();
                context.Database.Migrate();
            }
            return app;
        }
    }
}