
using System.Transactions;
using ATCMediator.Mediator.Interfaces;
using FinanceApp.Application.DTOs.Category;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.Services.Categories.CreateCategory
{
    public class CreateCategoryCommand : ICommand<CategoryDto>
    {

        public Guid UserId { get; set; } = Guid.Empty;
        public string Name { get; private set; } = string.Empty;
        public TransactionType Type { get; set; }
    }
}