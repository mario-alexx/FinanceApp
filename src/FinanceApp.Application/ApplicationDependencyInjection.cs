using ATCMediator;
using FinanceApp.Application.Interfaces;
using FinanceApp.Application.Services;
using FinanceApp.Application.Services.Categories.CreateCategory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceApp.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddATCMediator(
            typeof(CreateCategoryCommandHandler).Assembly
        );
        
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}
