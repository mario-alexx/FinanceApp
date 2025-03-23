using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinanceApp.Application.Common.Interfaces;
using FinanceApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FinanceApp.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public AuthService(UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IEmailService emailService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<bool> RegisterAsync(ApplicationUser user, string password)
    {
        IdentityResult result = await _userManager.CreateAsync(user, password);
        return result.Succeeded;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if(user == null || !await _userManager.CheckPasswordAsync(user, password))
            return null;
        
        return GenerateJwtToken(user);
    }
    
    private string GenerateJwtToken (ApplicationUser user)
    {
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.UtcNow.AddHours(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<bool> ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if(user == null) return false;
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var frontendUrl = _configuration["AppSettings:FrontendUrl"] ?? "https://localhost:5001/swagger";

        string resetLink = $"{frontendUrl}/reset-password?email={user.Email}&token={Uri.EscapeDataString(token)}";

        // Enviar el token por correo (TODO)
        await _emailService.SendEmailAsync(user.Email!, "Recuperar contraseña", 
        $"Haz click en el siguiente enlace para establecer tu contraseña: <a href='{resetLink}'>Restablecer</a>");

        return true;
    }

    public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
    {
       ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if(user == null)
            return false;

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded ? true : false;
    }
}
