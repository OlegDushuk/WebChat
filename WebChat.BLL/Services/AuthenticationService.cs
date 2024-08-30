using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebChat.BLL.Configuration;
using WebChat.BLL.Interfaces;
using WebChat.BLL.Models;
using WebChat.BLL.Models.DTOs;
using WebChat.BLL.Utils.Exceptions;
using WebChat.BLL.Utils.Exceptions.AuthenticateExceptions;
using WebChat.DAL.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebChat.BLL.Services;

public class AuthenticationService : IAuthenticationService
{
  private readonly JwtSettings _jwtSettings;
  private readonly IUserRepository _userRepository;

  public AuthenticationService(IOptions<JwtSettings> jwtSettings, IUserRepository userRepository)
  {
    _userRepository = userRepository;
    _jwtSettings = jwtSettings.Value;
  }

  public async Task<UserDto> AuthenticateUserAsync(UserLoginData data)
  {
    if (string.IsNullOrEmpty(data.Email) ||
        string.IsNullOrEmpty(data.Password))
      throw new InvalidDataException("Data is null or empty.");
    
    var user = await _userRepository.GetByEmail(data.Email);
    if (user == null)
      throw new UserNotFoundException(data.Email);

    if (user.PasswordHash != data.Password)
      throw new AuthenticationException("Invalid password.");

    return new UserDto
    {
      Username = user.Username,
      Email = user.Email,
      IsActive = user.IaActive,
      Name = user.Name
    };
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