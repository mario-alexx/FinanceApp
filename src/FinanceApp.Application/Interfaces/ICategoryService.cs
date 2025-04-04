using System;
using FinanceApp.Application.DTOs.Category;

namespace FinanceApp.Application.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync(Guid userId);
    Task<CategoryDto?> GetByIdAsync(Guid id, Guid userId);
    Task<CategoryDto> CreateAsync(CreateCategoryDto dto, Guid userId);
    Task<bool> UpdateAsync(UpdateCategoryDto dto, Guid userId);
    Task<bool> DeleteAsync(Guid id, Guid userId);
}
