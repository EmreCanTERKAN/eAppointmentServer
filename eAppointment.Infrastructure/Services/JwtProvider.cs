﻿using eAppointment.Application.Services;
using eAppointment.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eAppointment.Infrastructure.Services;

internal sealed class JwtProvider : IJwtProvider
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

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes("SADASDAS^'+sd^'FEWDFS"));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);

        JwtSecurityToken securityToken = new(
            issuer: "Emre Can",
            audience: "eAppointment",
            claims: claims,
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: signingCredentials);

        JwtSecurityTokenHandler handler = new();
        string token = handler.WriteToken(securityToken);
        return token;
    }
}
