using System;
using System.Collections.Generic;
using System.Security.Claims;

public interface IJwtTokenHelper
{
    string GenerateAccessToken(IEnumerable<Claim> claims, DateTime expiryTime);
    string GenerateRefreshToken();
}