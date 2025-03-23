namespace FinanceApp.Application.DTOs.Auth;

public record RegisterDto(string FirstName, string LastName, string Email, string Password, string ConfirmPassword);