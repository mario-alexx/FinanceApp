using System;
using System.Threading.Tasks;
using FinanceApp.Application.Common.Interfaces;
using FinanceApp.Application.DTOs.Auth;
using FinanceApp.Domain.Entities;
using FinanceApp.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FinanceApp.Tests.Services;

public class AuthServiceTests
{  
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly ApplicationUser _testUser;

    public AuthServiceTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _testUser = new ApplicationUser 
        {
            UserName = "testuser",
            Email = "test@example.com"
        };
    }
    
    #region  Register Tests
    [Fact]
    public async Task Register_UserValid_ShouldReturnTrue()
    {
        // Arrange
        _authServiceMock.Setup(s => s.RegisterAsync(It.IsAny<ApplicationUser>(), "Password123!"))
            .ReturnsAsync(true);

        // Act
        var result = await _authServiceMock.Object.RegisterAsync(_testUser, "Password123!");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Register_EmailAlreadyRegistered_ShouldReturnFalse()
    {
        // Arrange
        _authServiceMock.Setup(s => s.RegisterAsync(It.IsAny<ApplicationUser>(), "Password123!"))
            .ReturnsAsync(false);

        // Act
        var result = await _authServiceMock.Object.RegisterAsync(_testUser, "Password123!");

        // Assert
        Assert.False(result);
    }
    #endregion

    #region Login Tests
    [Fact]
    public async Task Login_ValidCredentials_ShouldReturnToken()
    {
        // Arrange
        _authServiceMock.Setup(s => s.LoginAsync("test@example.com", "Password123!"))
            .ReturnsAsync("mocked_jwt_token");

        // Act
        var result = await _authServiceMock.Object.LoginAsync("test@example.com", "Password123!");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("mocked_jwt_token", result);
    }

    [Fact]
    public async Task Login_InvalidCredentials_ShouldReturnNull()
    {
        // Arrange
        _authServiceMock.Setup(s => s.LoginAsync("test@example.com", "WrongPassword"))
            .ReturnsAsync((string?)null);

        // Act
        var result = await _authServiceMock.Object.LoginAsync("test@example.com", "WrongPassword");

        // Assert
        Assert.Null(result);
    }
    #endregion
    [Fact]
    public async Task ForgotPassword_ValidEmail_ShouldSendEmail()
    {
        // Arrange
        _authServiceMock.Setup(a => a.ForgotPasswordAsync("test@example.com"))
            .ReturnsAsync(true);

        // Act
        var result = await _authServiceMock.Object.ForgotPasswordAsync("test@example.com");

        // Assert
        Assert.True(result);
        _authServiceMock.Verify(a => a.ForgotPasswordAsync("test@example.com"), Times.Once);
    }

    [Fact]
    public async Task ForgotPassword_InvalidEmail_ShouldReturnFalse()
    {
        // Arrange
        _authServiceMock.Setup(a => a.ForgotPasswordAsync("invalid@example.com"))
            .ReturnsAsync(false);

        // Act
        var result = await _authServiceMock.Object.ForgotPasswordAsync("invalid@example.com");

        // Assert
        Assert.False(result);
        _authServiceMock.Verify(a => a.ForgotPasswordAsync("invalid@example.com"), Times.Once);
    }

    [Fact]
    public async Task ResetPassword_ValidData_ShouldResetPassword()
    {
        // Arrange
        _authServiceMock.Setup(a => a.ResetPasswordAsync("test@example.com", "valid-token", "NewPassword123!"))
            .ReturnsAsync(true);

        // Act
        var result = await _authServiceMock.Object.ResetPasswordAsync("test@example.com", "valid-token", "NewPassword123!");

        // Assert
        Assert.True(result);
        _authServiceMock.Verify(a => a.ResetPasswordAsync("test@example.com", "valid-token", "NewPassword123!"), Times.Once);
    }

    
    [Fact]
    public async Task ResetPassword_InvalidEmail_ShouldReturnFalse()
    {
        // Arrange
        _authServiceMock
            .Setup(a => a.ResetPasswordAsync("invalid@example.com", "valid-token", "NewPassword123!"))
            .ReturnsAsync(false);

        // Act
        var result = await _authServiceMock.Object.ResetPasswordAsync("invalid@example.com", "valid-token", "NewPassword123!");

        // Assert
        Assert.False(result);
        _authServiceMock.Verify(a => a.ResetPasswordAsync("invalid@example.com", "valid-token", "NewPassword123!"), Times.Once);
    }

    [Fact]
    public async Task ResetPassword_InvalidToken_ShouldReturnFalse()
    {
        // Arrange
        _authServiceMock
            .Setup(a => a.ResetPasswordAsync("test@example.com", "invalid-token", "NewPassword123!"))
            .ReturnsAsync(false);

        // Act
        var result = await _authServiceMock.Object.ResetPasswordAsync("test@example.com", "invalid-token", "NewPassword123!");

        // Assert
        Assert.False(result);
        _authServiceMock.Verify(a => a.ResetPasswordAsync("test@example.com", "invalid-token", "NewPassword123!"), Times.Once);
    }
}
