using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.DTOs.Transaction;

public class TransactionDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid? CategoryId { get; set; }

    public TransactionDto(Domain.Entities.Transaction transaction)
    {
        Id = transaction.Id;
        Amount = transaction.Amount;
        Type = transaction.Type;
        Date = transaction.CreatedAt;
        Description = transaction.Description;
        CategoryId = transaction.CategoryId;
    }
}

