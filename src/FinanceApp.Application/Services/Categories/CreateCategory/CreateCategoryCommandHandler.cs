
using ATCMediator.Mediator.Interfaces;
using FinanceApp.Application.DTOs.Category;
using FinanceApp.Application.Interfaces.Persistence;
using FinanceApp.Domain.Entities;

namespace FinanceApp.Application.Services.Categories.CreateCategory
{
    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand command)
        {
            var category = new Category(command.UserId, command.Name, command.Type);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return new CategoryDto(category);
        }
    }
}