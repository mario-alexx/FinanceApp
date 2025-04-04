using System;
using FinanceApp.Application.DTOs.Category;
using FinanceApp.Application.Interfaces;
using FinanceApp.Application.Interfaces.Persistence;
using FinanceApp.Domain.Entities;

namespace FinanceApp.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CategoryDto>> GetAllAsync(Guid userId)
    {
        var categories = await _unitOfWork.Categories.GetAllAsync(userId);
        return categories.Select(c => new CategoryDto(c) ).ToList();
    }

    public async Task<CategoryDto?> GetByIdAsync(Guid id, Guid userId)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id, userId);

        return category is null ? null : new CategoryDto(category);
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto, Guid userId)
    {
        var category = new Category(userId, dto.Name, dto.Type);
        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();
        return new CategoryDto(category);
    }

    public async Task<bool> UpdateAsync(UpdateCategoryDto dto, Guid userId)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(dto.Id, userId);
        if(category is null) return false;

        category.Update(dto.Name, dto.Type);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id, userId);
        if(category is null || category.UserId != userId) return false;

        _unitOfWork.Categories.Delete(category);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
