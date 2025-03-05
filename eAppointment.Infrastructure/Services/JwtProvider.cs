using eAppointment.Application.Services;
using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eAppointment.Infrastructure.Services;

internal sealed class JwtProvider(
    IConfiguration configuration,
    IUserRoleRepository userRoleRepository,
    RoleManager<AppRole> roleManager) : IJwtProvider
{
    public async Task<string> CreateTokenAsync(AppUser appUser, CancellationToken cancellationToken)
    {
        List<AppUserRole> appUserRoles = await userRoleRepository.Where(p => p.UserId == appUser.Id).ToListAsync(cancellationToken);

        List<AppRole> roles = new();

        foreach(var userRole in appUserRoles)
        {
            AppRole? role = await roleManager.Roles.Where(p => p.Id == userRole.RoleId).FirstOrDefaultAsync(cancellationToken);
            if (role is not null)
                roles.Add(role);
        }

        List<string?> stringRoles = roles.Select(p => p.Name).ToList();
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
            new Claim(ClaimTypes.Name,appUser.FullName),
            new Claim("Email",appUser.Email ?? string.Empty),
            new Claim("UserName", appUser.UserName ?? string.Empty),
            new Claim(ClaimTypes.Role,JsonSerializer.Serialize(stringRoles))
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
