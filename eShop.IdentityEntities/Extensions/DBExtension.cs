using eShop.IdentityEntities.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.IdentityEntities.Extensions
{
    public static class DBExtension
    {
        public static IServiceCollection AddSQLContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IApplicationBuilder InitDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<IdentityContext>();
                context.Database.Migrate();
            }
            return app;
        }
    }
}
