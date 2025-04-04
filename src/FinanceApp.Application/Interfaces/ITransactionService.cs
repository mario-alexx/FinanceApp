using System.Transactions;
using FinanceApp.Application.DTOs.Transaction;

namespace FinanceApp.Application.Interfaces;

public interface ITransactionService
{
    Task<List<TransactionDto>> GetAllAsync(Guid userId);
    Task<TransactionDto?> GetByIdAsync(Guid id, Guid userId);
    Task<TransactionDto> CreateAsync(CreateTransactionDto dto, Guid userId);
    Task<bool> UpdateAsync(UpdateTransactionDto dto, Guid userId);
    Task<bool> DeleteAsync(Guid id, Guid userId);
}

