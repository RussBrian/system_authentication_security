using authentication_security.Core.Application.Interfaces;
using authentication_security.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace authentication_security.Infraestructure;
public static class ServiceRegistration
{
    public static void AddInfraestructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        #region Database Configuration
        services.AddDbContext<AppContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
               m => m.MigrationsAssembly(typeof(AppContext).Assembly.FullName)),
               ServiceLifetime.Scoped);
        #endregion

        #region Repositories Dependecy
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        #endregion

    }
}
