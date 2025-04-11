using System;
using FinanceApp.Application.Common.Pagination;
using FinanceApp.Application.DTOs.Transaction;
using FinanceApp.Domain.Entities;

namespace FinanceApp.Application.Interfaces.Persistence;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetAllAsync(Guid userId);
    Task<Transaction?> GetByIdAsync(Guid id, Guid userId);
    IQueryable<Transaction> Query();
    Task<PagedResult<Transaction>> GetFilteredAsync(Guid id, TransactionFilterDto filters, PaginationParams paginationParams);
    Task AddAsync(Transaction transaction);
    void Update(Transaction transaction);
    void SoftDelete(Transaction transaction);
    Task<List<Transaction>> GetDeletedAsync(Guid userId);
}
