using FinanceApp.Application.Common.Interfaces;
using FinanceApp.Application.DTOs.Auth;
using FinanceApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto user)
        {
            if(user.Password != user.ConfirmPassword) 
                return BadRequest("Passwords do not match");

            var userApplication = new ApplicationUser {  Email = user.Email, UserName = user.Email, FullName = user.FirstName + " " + user.LastName }; 
            bool result = await _authService.RegisterAsync(userApplication, user.Password);
            
            if(!result) return BadRequest("Registration error");

            return Ok( "Successfully registered user" );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user) 
        {
            
            string? token = await _authService.LoginAsync(user.Email, user.Password);
            if(token == null)
                return Unauthorized("Invalid Credentials");

            return Ok(new { Token = token} );
        }   

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto request)
        {
            var success = await _authService.ForgotPasswordAsync(request.Email);
            if(!success) return BadRequest("User not found");
            return Ok( "Recovery email sent." );
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
        {
            bool result = await _authService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
            if(!result) return BadRequest("No se pudo restablecer la contraseña");

            return Ok( "Contraseña restablecida correctamente" );
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult Test() => Ok("Validación funcionando");
    }
}
