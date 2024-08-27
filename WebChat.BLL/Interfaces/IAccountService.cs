using WebChat.BLL.Models;

namespace WebChat.BLL.Interfaces;

public interface IAccountService
{
  Task RegisterUserAsync(UserRegistrationData data);
}