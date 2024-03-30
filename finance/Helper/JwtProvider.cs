using finance.Models;

namespace finance.Helper;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


public sealed class JwtProvider(IOptions<JwtOptions> options)
    : IJwtProvider
{
    public async Task<string> GenerateAsync(Benutzer user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Name.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            
        };
        
        

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(options.Value.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            options.Value.Issuer,
            options.Value.Audience,
            claims,
            null,
            DateTime.UtcNow.AddDays(1),
            signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}