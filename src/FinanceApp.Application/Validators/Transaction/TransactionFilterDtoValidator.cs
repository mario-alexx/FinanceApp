using System;
using System.Data;
using FinanceApp.Application.DTOs.Transaction;
using FluentValidation;

namespace FinanceApp.Application.Validators.Transaction;

public class TransactionFilterDtoValidator : AbstractValidator<TransactionFilterDto>
{
    public TransactionFilterDtoValidator()
    {
        RuleFor(f => f.MinAmount)
            .GreaterThanOrEqualTo(0).When(f => f.MinAmount.HasValue)
            .WithMessage("Min amount must be greather than or equal to 0.");
        
        RuleFor(f => f.MaxAmount)
            .GreaterThanOrEqualTo(0).When(f => f.MaxAmount.HasValue)
            .WithMessage("Max amount must be greather than or equal to 0.");
        
        RuleFor(f => f)
            .Must(f => !f.MinAmount.HasValue || !f.MaxAmount.HasValue || f.MinAmount <= f.MaxAmount)
            .WithMessage("Min amount must be less than or equal to Max amount.");
        
        RuleFor(f => f.StartDate)
            .LessThanOrEqualTo(f => f.EndDate)
            .When(f => f.StartDate.HasValue && f.EndDate.HasValue)
            .WithMessage("Start date must be before or equal to End date");

        RuleFor(f => f.Description)
            .MaximumLength(200).When(f => !string.IsNullOrWhiteSpace(f.Description))
            .WithMessage("Description can't be longer than 200 characters.");

    }
}
