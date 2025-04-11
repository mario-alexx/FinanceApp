using System;
using FinanceApp.Application.Common.Pagination;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Infrastructure.Persistence.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, PaginationParams pagination)
    {
        return query 
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize);
    }

    public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, PaginationParams paginationParams)
    {   
       if(string.IsNullOrWhiteSpace(paginationParams.OrderBy))
            return query.OrderByDescending(e => EF.Property<object>(e!, "Date"));

       return paginationParams.IsDescending
            ? query.OrderByDescending(e => EF.Property<object>(e!, paginationParams.OrderBy))
            : query.OrderBy(e => EF.Property<object>(e!, paginationParams.OrderBy));
    }
}
