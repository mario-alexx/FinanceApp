using System;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.DTOs.Category;

public class CreateCategoryDto
{
    public string Name { get; set;} = null!;

    public TransactionType Type { get; set; }

}
