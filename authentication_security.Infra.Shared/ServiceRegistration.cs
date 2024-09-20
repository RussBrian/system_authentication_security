using authentication_security.Core.Application.Interfaces;
using authentication_security.Core.Domain.Settings;
using authentication_security.Infra.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace authentication_security.Infra.Shared;

public static class ServiceRegistration
{
    public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<IEmailService, EmailService>();
    }
}