using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Wavy.Application.Abstractions;
using Wavy.Domain.Users;

namespace Wavy.Infrastructure.Authencation;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
    {
        jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(User user)
    {
        // 1. Создаем "Клеймы" (Claims) - информацию о пользователе внутри токена
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Sub = Subject (ID пользователя)
            new(JwtRegisteredClaimNames.GivenName, user.Username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Уникальный ID самого токена
        };

        // 2. Создаем ключ подписи из нашего Секрета
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 3. Собираем токен
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}