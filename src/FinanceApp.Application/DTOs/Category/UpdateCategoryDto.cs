using System;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.DTOs.Category;


public class UpdateCategoryDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public TransactionType Type { get; set; }
}
