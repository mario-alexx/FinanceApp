using System;
using FinanceApp.Domain.Entities;

namespace FinanceApp.Application.Interfaces.Persistence;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetAllAsync(Guid userId);
    Task<Transaction?> GetByIdAsync(Guid id, Guid userId);
    Task AddAsync(Transaction transaction);
    void Update(Transaction transaction);
    void SoftDelete(Transaction transaction);
}
