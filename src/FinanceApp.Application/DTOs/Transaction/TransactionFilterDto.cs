using System;
using FinanceApp.Domain.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace FinanceApp.Application.DTOs.Transaction;

public class TransactionFilterDto
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid? CategoryId { get; set; }
    public TransactionType? Type { get; set; }
    public string? Description { get; set; }    

    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
}
