using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Backend.Controllers;
using Microsoft.AspNetCore.Authorization;

public class AuthController : BaseApiController
{
    public AuthController(IRegisterRepository registerRepository) : base(registerRepository)
    {
    }

    [HttpGet("signin-google")]
    public IActionResult SignInWithGoogle()
    {
        var redirectUrl = Url.Action("GoogleResponse", "Auth", null, Request.Scheme);
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        Console.WriteLine($"{redirectUrl}");
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }
    [HttpGet("google-response")]
    public async Task<IActionResult> GoogleResponse()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!authenticateResult.Succeeded)
            return BadRequest();

        var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
        var name = authenticateResult.Principal.FindFirst(ClaimTypes.Name)?.Value;

        // Only allow emails ending with mlrit.ac.in
        //if (string.IsNullOrEmpty(email) || !email.EndsWith("@mlrit.ac.in", StringComparison.OrdinalIgnoreCase))
        //{
        //    return Unauthorized("Only mlrit.ac.in accounts are allowed.");
        //}

        var user = await _registerRepository.GetUser(email);
        string userRole = "user"; // Default role
        
        if(user == null) {
            // Create a new user if not found
            var newUser = new User
            {
                Email = email,
                Role = "user", // Default role for new users
                CreatedAt = DateTime.UtcNow
            };
            user = await _registerRepository.CreateUser(newUser);
        } else {
            // Get the existing user's role
            userRole = user.Role;
        }

        // Generate JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("B3c!X7e@1qT9z$wL4pV2mN8rS6uJ0hK5"); // Replace with your key or store in config
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, name ?? ""),
                new Claim(ClaimTypes.Email, email ?? ""),
                new Claim(ClaimTypes.Role, userRole),
                new Claim("role", userRole) // Adding an additional claim for easy access in frontend
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        // Redirect to frontend with JWT as query param
        return Redirect($"http://localhost:4200?token={jwt}");
    }
    
    [HttpPost("set-admin")]
    [Authorize(Roles = "admin")] // Only admins can set other users as admin
    public async Task<IActionResult> SetAdmin([FromBody] SetAdminRequest request)
    {
        if (string.IsNullOrEmpty(request.Email))
            return BadRequest("Email is required");
            
        var result = await _registerRepository.SetUserRole(request.Email, "admin");
        if (!result)
            return NotFound("User not found");
            
        return Ok("User has been set as admin");
    }
}

public class SetAdminRequest
{
    public string Email { get; set; } = string.Empty;
}