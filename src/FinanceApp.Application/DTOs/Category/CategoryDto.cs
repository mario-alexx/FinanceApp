using System;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.DTOs.Category;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TransactionType Type { get; set; }

    public CategoryDto(Domain.Entities.Category category)
    {
        Id = category.Id;
        Name = category.Name;
        Type = category.Type;
    }
}

