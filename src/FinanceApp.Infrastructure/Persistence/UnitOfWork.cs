using System;
using FinanceApp.Application.Interfaces.Persistence;

namespace FinanceApp.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public ITransactionRepository Transactions {get; }
    public ICategoryRepository Categories  {get ;}

    public UnitOfWork(AppDbContext context, ITransactionRepository transactions, ICategoryRepository categories)
    {
        _context = context;
        Transactions = transactions;
        Categories = categories;
    }

    public async Task<int> SaveChangesAsync() =>  await _context.SaveChangesAsync();
}
