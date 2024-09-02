using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebChat.BLL.Configuration;
using WebChat.BLL.Interfaces;
using WebChat.BLL.Models;
using WebChat.BLL.Models.DTOs;
using WebChat.BLL.Utils.Exceptions.AuthenticateExceptions;
using WebChat.BLL.Utils.Helpers;
using WebChat.DAL.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.AspNetCore.Http;

namespace WebChat.BLL.Services;

public class AuthenticationService : IAuthenticationService
{
  private readonly JwtSettings _jwtSettings;
  private readonly IUserRepository _userRepository;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public AuthenticationService(IOptions<JwtSettings> jwtSettings, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
  {
    _userRepository = userRepository;
    _httpContextAccessor = httpContextAccessor;
    _jwtSettings = jwtSettings.Value;
  }

  public async Task<UserDto> AuthenticateUserAsync(UserLoginData data)
  {
    if (string.IsNullOrEmpty(data.Email) ||
        string.IsNullOrEmpty(data.Password))
      throw new InvalidDataException("Email and password required.");
    
    var user = await _userRepository.GetByEmail(data.Email);
    
    if (user == null)
      throw new UserNotFoundByEmailException(data.Email);
    
    if (!PasswordHasher.VerifyPassword(user.PasswordHash!, data.Password))
      throw new InvalidPasswordException();

    return new UserDto
    {
      Username = user.Username,
      Email = user.Email
    };
  }
  
  public string GetJwtToken(string username)
  {
    var claims = new[]
    {
      new Claim(ClaimTypes.Name, username),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };
    
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey!));
    var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    
    var token = new JwtSecurityToken(
      issuer: _jwtSettings.Issuer,
      audience: _jwtSettings.Audience,
      claims: claims,
      expires: DateTime.Now.AddMinutes(30),
      signingCredentials: creeds);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}