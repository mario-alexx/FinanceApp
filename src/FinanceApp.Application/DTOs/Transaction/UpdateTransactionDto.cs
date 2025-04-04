using System;
using System.ComponentModel.DataAnnotations;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.DTOs.Transaction;

public class UpdateTransactionDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public TransactionType Type { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    public string Description { get; set; } = string.Empty;
}
