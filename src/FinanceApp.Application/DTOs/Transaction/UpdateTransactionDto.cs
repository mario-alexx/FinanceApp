using System;
using System.ComponentModel.DataAnnotations;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.DTOs.Transaction;

public class UpdateTransactionDto
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }

    public DateTime Date { get; set; }

    public Guid CategoryId { get; set; }

    public string Description { get; set; } = string.Empty;
}
