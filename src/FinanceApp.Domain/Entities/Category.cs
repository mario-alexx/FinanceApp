using System;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public TransactionType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    private Category() { } // Constructor required by EF Core

    public Category(Guid userId, string name, TransactionType type)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Type = type;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, TransactionType type) 
    {
        Name = name;
        Type = type;
        UpdatedAt = DateTime.UtcNow;
    }
}
