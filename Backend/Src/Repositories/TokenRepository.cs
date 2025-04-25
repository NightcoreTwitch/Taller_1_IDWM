using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Backend.Src.Interfaces;
using Backend.Src.Models;

namespace Backend.Src.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly IConfiguration _configuration;

    public TokenRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GenerateTokenAsync(User user, string roleId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Names + " " + user.LastNames),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, roleId),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}