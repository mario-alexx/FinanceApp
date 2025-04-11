using System;
using FinanceApp.Application.Common.Pagination;
using FinanceApp.Application.DTOs.Transaction;
using FinanceApp.Application.Interfaces;
using FinanceApp.Application.Interfaces.Persistence;
using FinanceApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceApp.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<TransactionDto>> GetAllAsync(Guid userId)
    {
        var transactions = await _unitOfWork.Transactions.GetAllAsync(userId);
        return transactions.Select(t => new TransactionDto(t)).ToList();
    }

    public async Task<TransactionDto?> GetByIdAsync(Guid id, Guid userId)
    {
        var transaction = await _unitOfWork.Transactions.GetByIdAsync(id, userId);
        return transaction is null ? null : new TransactionDto(transaction);
    }

    public async Task<TransactionDto> CreateAsync(CreateTransactionDto dto, Guid userId)
    {
        var transaction = new Transaction(userId, dto.CategoryId, dto.Amount, dto.Description, dto.Type, dto.Date);
        await _unitOfWork.Transactions.AddAsync(transaction);
        await _unitOfWork.SaveChangesAsync();
        return new TransactionDto(transaction);
    }

    public async Task<bool> UpdateAsync(UpdateTransactionDto dto, Guid userId)
    {
        var transaction = await _unitOfWork.Transactions.GetByIdAsync(dto.Id, userId);
        if (transaction == null) return false;

        transaction.Update(dto.CategoryId, dto.Amount, dto.Description, dto.Type, dto.Date);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var transaction = await _unitOfWork.Transactions.GetByIdAsync(id, userId);
        if (transaction is null) return false;

        _unitOfWork.Transactions.SoftDelete(transaction);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<List<TransactionDto>> GetDeletedAsync(Guid userId)
    {
        var deletedTransactions = await _unitOfWork.Transactions.GetDeletedAsync(userId);
        return deletedTransactions.Select(t => new TransactionDto(t)).ToList();
    }

    public async Task<PagedResult<TransactionDto>> GetFilteredAsync(Guid userId, TransactionFilterDto filters, PaginationParams paginationParams) 
    {
        var pagedTransactions = await _unitOfWork.Transactions.GetFilteredAsync(userId, filters, paginationParams);        
        
        var dtoItems = pagedTransactions.Items.Select(t => new TransactionDto(t)).ToList();

        return new PagedResult<TransactionDto>(
            dtoItems, 
            pagedTransactions.TotalCount,
            pagedTransactions.PageNumber, 
            pagedTransactions.PageSize
        );
    }
}

