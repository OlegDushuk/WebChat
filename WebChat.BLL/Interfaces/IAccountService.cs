using WebChat.BLL.Models;
using WebChat.BLL.Models.Views;

namespace WebChat.BLL.Interfaces;

public interface IAccountService
{
  Task RegisterUserAsync(UserRegistrationData data);
  Task<UserView?> GetUserViewAsync(string username);
  Task DeleteAccount(string username);
}