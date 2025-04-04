using FinanceApp.Application.Interfaces.Persistence;
using FinanceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Infrastructure.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Transaction>> GetAllAsync(Guid userId)
    {
        return await _context.Transactions
            .Where(t => t.UserId == userId && !t.IsDeleted)
            .ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(Guid id, Guid userId)
    {
        return await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId && !t.IsDeleted);
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
    }

    public void Update(Transaction transaction)
    {
        _context.Entry(transaction).State = EntityState.Modified;
    }

    public void SoftDelete(Transaction transaction)
    {
        transaction.Delete(); // Apply Soft Delete from entity
        _context.Entry(transaction).State = EntityState.Modified;
    }

}
