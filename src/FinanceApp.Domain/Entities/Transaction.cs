using FinanceApp.Domain.Enums;

namespace FinanceApp.Domain.Entities;

public class Transaction 
{
    public Guid Id { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public Guid UserId { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public DateTime Date {get; private set;}
    public TransactionType Type { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    private Transaction() { } // Constructor required by EF Core

    public Transaction(Guid userId, Guid categoryId, decimal amount, string description, TransactionType type, DateTime date)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        CategoryId = categoryId;
        Amount = amount;
        Description = description;
        Type = type;
        Date = date;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }

    public void Update(Guid categoryId, decimal amount, string description, TransactionType type, DateTime date)
    {
        Amount = amount;
        Description = description;
        Type = type;
        Date = date;
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete() 
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    } 
}
