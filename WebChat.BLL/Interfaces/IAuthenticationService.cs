using WebChat.BLL.Models;
using WebChat.BLL.Models.DTOs;

namespace WebChat.BLL.Interfaces;

public interface IAuthenticationService
{
  Task<UserDto> AuthenticateUserAsync(UserLoginData data);
  string GetJwtToken(string username);
}