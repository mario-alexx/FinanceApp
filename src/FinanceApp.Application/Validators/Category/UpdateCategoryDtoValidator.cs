using System;
using FinanceApp.Application.DTOs.Category;
using FluentValidation;

namespace FinanceApp.Application.Validators.Category;

public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        
        RuleFor(c => c.Type)
            .IsInEnum().WithMessage("Invalid transaction type.");
    }
}
