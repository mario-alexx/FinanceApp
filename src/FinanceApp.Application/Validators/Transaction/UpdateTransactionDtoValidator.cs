using System;
using System.Security.Claims;
using FinanceApp.Application.DTOs.Transaction;
using FinanceApp.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace FinanceApp.Application.Validators.Transaction;

public class UpdateTransactionDtoValidator : AbstractValidator<UpdateTransactionDto>
{
    public UpdateTransactionDtoValidator(ICategoryService categoryService, IHttpContextAccessor httpContextAccessor)
    {
        RuleFor(t => t.Id)
            .NotEmpty().WithMessage("Transaction ID is required");

        RuleFor(t => t.Amount)
            .GreaterThan(0).WithMessage("Amount must be greather than 0.");

        RuleFor(t => t.Description)
            .MaximumLength(100).WithMessage("Description cannot exceed 100 characters.");

        RuleFor(t => t.Type)
            .IsInEnum().WithMessage("Invalid transaction type.");
        
        RuleFor(t => t.Date)
            .NotEmpty().WithMessage("Date is required")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date cannot be in the future");

                RuleFor(t => t.CategoryId)
            .MustAsync(async (categoryId, cancellation) => 
            {
                // if(categoryId == null) return true;

                var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if(userIdClaim == null) return false;

                var userId = Guid.Parse(userIdClaim.Value);
                return await categoryService.ExistsAsync(categoryId, userId);
            })
            .WithMessage("The specified category does not exist.");
    }
}
