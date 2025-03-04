using eAppointment.Application.Services;
using eAppointment.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eAppointment.Infrastructure.Services;

internal sealed class JwtProvider(
    IConfiguration configuration) : IJwtProvider
{
    public string CreateToken(AppUser appUser)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
            new Claim(ClaimTypes.Name,appUser.FullName),
            new Claim("Email",appUser.Email ?? string.Empty),
            new Claim("UserName", appUser.UserName ?? string.Empty)
        };

        DateTime expires = DateTime.Now.AddDays(1);

        SymmetricSecurityKey securityKey = 
            new(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:SecretKey").Value ?? ""));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);

        JwtSecurityToken securityToken = new(
            issuer: configuration.GetSection("Jwt:Issuer").Value,
            audience: configuration.GetSection("Jwt:Audience").Value,
            claims: claims,
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: signingCredentials);

        JwtSecurityTokenHandler handler = new();
        string token = handler.WriteToken(securityToken);
        return token;
    }
}
