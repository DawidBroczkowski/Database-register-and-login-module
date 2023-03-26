using Euvic.Application.Interfaces;
using Euvic.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Euvic.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly(typeof(UserContext).Assembly.FullName)), ServiceLifetime.Transient);

            services.AddTransient<IUserRepository, DbUserRepository>();
            
            return services;
        }

        public static IServiceProvider EnsureDatabaseIsCreated(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<UserContext>();
                dbContext.Database.EnsureCreated();
            }

            return services;
        }
    }
}
