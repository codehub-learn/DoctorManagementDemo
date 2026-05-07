using DoctorApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoctorApi.Security;

[Route("api/[controller]")]
[ApiController]
public class SecurityController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public SecurityController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public  IActionResult  Login([FromBody] LoginRequest request)
    {
        // Validate the login credentials (this is just a placeholder, implement your own logic)
        if (request.Username == "admin" && request.Password == "password")
        {
            // Generate a JWT token (this is just a placeholder, implement your own logic)
            var token = GenerateJwtToken(request.Username);
            return Ok(new { Token = token });
        }
        else
        {
            return Unauthorized();
        }
    }


    [HttpGet("doctorMessage")]
    [Authorize]
    public IActionResult GetProtectedData()
    {
        // This endpoint is protected and requires a valid JWT token
        return Ok(new { Message = "This is protected data." });
    }



    private string GenerateJwtToken(string username)
    {
        // Implement your JWT token generation logic here
        // This is just a placeholder and should be replaced with actual implementation
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("Username cannot be null or empty", nameof(username));
        }

        // Read values from appsettings.json
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var masterKey = _configuration["Jwt:Key"] ?? "";
        var expiryMinutes =
            Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"]);

        // Create claims
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "Dimitris"),
            new Claim(ClaimTypes.Email, "dimitris@example.com"),
            new Claim(ClaimTypes.Role, "Admin")
        };

        // Create signing key
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(masterKey));

        // Create signing credentials
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        // Create token
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials);

        // Return JWT string
        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}

