using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;
using RTOWebLMS.Models;

namespace RTOWebLMS.Controllers;

[ApiController]
[Route("api/[controller]")]
[IgnoreAntiforgeryToken]
public class AccountController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(SignInManager<User> signInManager, ILogger<AccountController> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _signInManager.PasswordSignInAsync(
                request.Email,
                request.Password,
                request.RememberMe,
                lockoutOnFailure: true
            );

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} logged in successfully", request.Email);
                return Ok(new { success = true, message = "Login successful" });
            }
            else if (result.IsLockedOut)
            {
                return BadRequest(new { success = false, message = "Account locked due to multiple failed login attempts. Please try again in 15 minutes." });
            }
            else if (result.RequiresTwoFactor)
            {
                return BadRequest(new { success = false, message = "Two-factor authentication required." });
            }
            else
            {
                return BadRequest(new { success = false, message = "Invalid email or password." });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for {Email}", request.Email);
            return StatusCode(500, new { success = false, message = $"An error occurred during login: {ex.Message}" });
        }
    }

    [HttpPost("login-form")]
    public async Task<IActionResult> LoginForm([FromForm] string email, [FromForm] string password, [FromForm] bool rememberMe = false)
    {
        try
        {
            var result = await _signInManager.PasswordSignInAsync(
                email,
                password,
                rememberMe,
                lockoutOnFailure: true
            );

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} logged in successfully via form", email);
                return Redirect("/dashboard");
            }
            else if (result.IsLockedOut)
            {
                return Redirect("/login?error=locked");
            }
            else
            {
                return Redirect("/login?error=invalid");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during form login for {Email}", email);
            return Redirect("/login?error=server");
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out");
        return Ok(new { success = true, message = "Logout successful" });
    }
}

public class LoginRequest
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public bool RememberMe { get; set; }
}
