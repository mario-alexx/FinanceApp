using FinanceApp.Application.DTOs.Transaction;
using FinanceApp.Application.Interfaces;
using FinanceApp.Application.Services;
using FinanceApp.Application.Validators.Transaction;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceApp.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ICategoryService, CategoryService>();

        // FluentValidation
        services.AddValidatorsFromAssemblyContaining<CreateTransactionDtoValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        return services;
    }
}
