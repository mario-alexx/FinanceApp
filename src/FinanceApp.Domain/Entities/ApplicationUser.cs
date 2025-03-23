using System;
using Microsoft.AspNetCore.Identity;

namespace FinanceApp.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
