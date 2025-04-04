using System;

namespace FinanceApp.Application.Interfaces.Persistence;

public interface IUnitOfWork
{
    ITransactionRepository Transactions {get; }
    ICategoryRepository Categories {get; }

    Task<int> SaveChangesAsync();
}
