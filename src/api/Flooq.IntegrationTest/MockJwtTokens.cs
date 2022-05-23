using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Flooq.IntegrationTest;

/// <summary>
/// Mock JWT tokens for test user in integration tests.
/// </summary>
public static class MockJwtTokens
{
  private static readonly JwtSecurityTokenHandler s_tokenHandler = new();
  private static readonly RandomNumberGenerator s_rng = RandomNumberGenerator.Create();
  private static readonly byte[] s_key = new byte[32];

  public static string Issuer { get; } = Guid.NewGuid().ToString();
  public static SecurityKey SecurityKey { get; }
  public static SigningCredentials SigningCredentials { get; }

  static MockJwtTokens()
  {
    s_rng.GetBytes(s_key);
    SecurityKey = new SymmetricSecurityKey(s_key) { KeyId = Guid.NewGuid().ToString() };
    SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
  }

  public static string GenerateJwtToken(IEnumerable<Claim> claims)
  {
    return s_tokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, claims, null, DateTime.UtcNow.AddMinutes(20), SigningCredentials));
  } 
}