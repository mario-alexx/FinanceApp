using FinanceApp.Domain.Entities;

namespace FinanceApp.Application.Interfaces.Persistence;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(Guid userId);
    Task<Category?> GetByIdAsync(Guid id, Guid userId);
    Task AddAsync(Category category);
    void Update(Category category);
    void Delete(Category category);
}
