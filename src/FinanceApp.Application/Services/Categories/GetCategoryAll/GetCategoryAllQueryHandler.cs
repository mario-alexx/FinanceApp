using ATCMediator.Mediator.Interfaces;
using FinanceApp.Application.DTOs.Category;
using FinanceApp.Application.Interfaces.Persistence;

namespace FinanceApp.Application.Services.Categories.GetCategoryAll
{
    public class GetCategoryAllQueryHandler : IQueryHandler<GetCategoryAllQuery, IEnumerable<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryAllQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoryAllQuery query)
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(query.UserId);
            return categories.Select(c => new CategoryDto(c) ).ToList();
        }
    }
}