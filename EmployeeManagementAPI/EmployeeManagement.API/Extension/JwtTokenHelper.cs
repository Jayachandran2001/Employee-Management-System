using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtTokenHelper : IJwtTokenHelper
{
    private readonly string _secret;
    private readonly int _jwtLifespan;

    public JwtTokenHelper(IConfiguration configuration)
    {
        _secret = configuration["JwtSettings:Secret"];
        _jwtLifespan = int.Parse(configuration["JwtSettings:TokenLifespanMinutes"]);
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims, DateTime expiryTime)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expiryTime,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}
