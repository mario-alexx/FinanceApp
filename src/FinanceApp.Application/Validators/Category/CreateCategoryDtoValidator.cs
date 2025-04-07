using System;
using FinanceApp.Application.DTOs.Category;
using FluentValidation;

namespace FinanceApp.Application.Validators.Category;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(c => c.Type)
            .IsInEnum().WithMessage("Invalid transaction type.");
    }
}
