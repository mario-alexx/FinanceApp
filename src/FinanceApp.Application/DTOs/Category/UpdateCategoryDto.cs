using System;
using System.ComponentModel.DataAnnotations;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.DTOs.Category;


public class UpdateCategoryDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    public TransactionType Type { get; set; }
}
