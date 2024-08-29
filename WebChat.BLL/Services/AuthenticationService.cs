using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebChat.BLL.Configuration;
using WebChat.BLL.Interfaces;
using WebChat.BLL.Models.DTOs;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebChat.BLL.Services;

public class AuthenticationService : IAuthenticationService
{
  private readonly JwtSettings _jwtSettings;

  public AuthenticationService(IOptions<JwtSettings> jwtSettings)
  {
    _jwtSettings = jwtSettings.Value;
  }

  public string GetJwtToken(UserDto user, string audience)
  {
    var claims = new[]
    {
      new Claim(ClaimTypes.Name, user.Username!),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey!));
    var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: _jwtSettings.Issuer,
      audience: audience,
      claims: claims,
      expires: DateTime.Now.AddMinutes(30),
      signingCredentials: creeds);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}