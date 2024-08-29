using WebChat.BLL.Models.DTOs;

namespace WebChat.BLL.Interfaces;

public interface IAuthenticationService
{
  string GetJwtToken(UserDto user, string audience);
}