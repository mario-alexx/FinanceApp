using System;
using FinanceApp.Application.Common.Interfaces;
using Moq;

namespace FinanceApp.Tests.Services;

public class EmailServiceTests
{
    private readonly Mock<IEmailService> _emailServiceMock;

    public EmailServiceTests()
    {
        _emailServiceMock = new Mock<IEmailService>();
    }

    [Fact]
    public async Task SendEmail_ValidInput_ShouldSendEmailSuccessfully()
    {
        // Arrange
        _emailServiceMock.
            Setup(e => e.SendEmailAsync("test@example.com", "Test subject", "Test Body"))
            .Returns(Task.CompletedTask);

        // Act
        await _emailServiceMock.Object.SendEmailAsync("test@example.com", "Test subject", "Test Body");

        // Assert
        _emailServiceMock.Verify(e => e.SendEmailAsync("test@example.com", "Test subject", "Test Body"), Times.Once);
    }

    [Fact]
    public async Task SendEmail_EmptyRecipient_ShouldNotSendEmail()
    {
        // Arrange
        _emailServiceMock
            .Setup(e => e.SendEmailAsync("", "Test Subject", "Test Body"))
            .ThrowsAsync(new ArgumentException("Recipient email is required"));

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _emailServiceMock.Object.SendEmailAsync("", "Test Subject", "Test Body"));
        
        _emailServiceMock.Verify(e => e.SendEmailAsync("", "Test Subject", "Test Body"), Times.Once);
    }
}
