using FinanceApp.Application.Common.Pagination;
using FinanceApp.Application.DTOs.Transaction;
using FinanceApp.Application.Interfaces.Persistence;
using FinanceApp.Domain.Entities;
using FinanceApp.Infrastructure.Persistence.Extensions;
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

    public IQueryable<Transaction> Query() 
    {
        return _context.Transactions.AsQueryable();
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

    public async Task<List<Transaction>> GetDeletedAsync(Guid userId) 
    {
        return await _context.Transactions
            .Where(t => t.UserId == userId && t.IsDeleted)
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    public async Task<PagedResult<Transaction>> GetFilteredAsync(Guid userId, TransactionFilterDto filters, PaginationParams paginationParams)
    {
        var query = _context.Transactions
            .AsNoTracking()
            .Where(t => t.UserId == userId && !t.IsDeleted);

        if(filters.StartDate.HasValue)
            query = query.Where(t => t.Date >= filters.StartDate.Value);
            
        if(filters.EndDate.HasValue)
            query = query.Where(t => t.Date <= filters.EndDate.Value);

        if(filters.CategoryId.HasValue)
            query = query.Where(t => t.CategoryId == filters.CategoryId.Value);

        if(filters.Type.HasValue)
            query = query.Where(t => t.Type == filters.Type.Value);

        if(!string.IsNullOrWhiteSpace(filters.Description))
            query = query.Where(t => t.Description.ToLower().Contains(filters.Description.ToLower()));
        
        if(filters.MinAmount.HasValue)
            query = query.Where(t => t.Amount >= filters.MinAmount.Value);
        
        if(filters.MaxAmount.HasValue)
            query = query.Where(t => t.Amount <= filters.MaxAmount.Value);
        

        query = query.ApplyOrdering(paginationParams);
        
        // Total antes de paginar
        var totalCount = await query.CountAsync();

        // Ordenamiento y Paginación
        query = query.ApplyPagination(paginationParams);
        
        var items = await query.ToListAsync();

        return new PagedResult<Transaction>(items, totalCount, paginationParams.PageNumber, paginationParams.PageSize);
    }
}
