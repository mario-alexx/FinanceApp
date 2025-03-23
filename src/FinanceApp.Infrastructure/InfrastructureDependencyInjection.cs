using FinanceApp.Application.Common.Interfaces;
using FinanceApp.Infrastructure.Persistence;
using FinanceApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceApp.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IEmailService, SendGridEmailService>();
        
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"))); 
        
        return services;
    }
}
