using ATCMediator.Mediator.Interfaces;
using FinanceApp.Application.DTOs.Category;

namespace FinanceApp.Application.Services.Categories.GetCategoryAll
{
    public class GetCategoryAllQuery : IQuery<IEnumerable<CategoryDto>> { 
        public Guid UserId { get; set; } = Guid.Empty;
    }
}