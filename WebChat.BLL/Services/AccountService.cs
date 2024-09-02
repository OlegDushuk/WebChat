using WebChat.BLL.Interfaces;
using WebChat.BLL.Models;
using WebChat.BLL.Models.Views;
using WebChat.BLL.Utils.Exceptions.RegistrationExceptions;
using WebChat.BLL.Utils.Helpers;
using WebChat.DAL.Interfaces;
using WebChat.DAL.Models;

namespace WebChat.BLL.Services;

public class AccountService : IAccountService
{
  private readonly IUserRepository _userRepository;

  public AccountService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }
  
  public async Task RegisterUserAsync(UserRegistrationData data)
  {
    if (string.IsNullOrEmpty(data.Email) ||
        string.IsNullOrEmpty(data.Username) ||
        string.IsNullOrEmpty(data.Password))
      throw new InvalidDataException("Data is null or empty.");
    
    var emailTask = await _userRepository.GetByEmail(data.Email);
    if (emailTask != null)
      throw new EmailAlreadyInUseException(data.Email);
    
    var usernameTask = await _userRepository.GetByUsername(data.Username);
    if (usernameTask != null)
      throw new UsernameAlreadyTakenException(data.Username);

    var user = new User
    {
      Id = Guid.NewGuid(),
      Email = data.Email,
      Username = data.Username,
      PasswordHash = PasswordHasher.HashPassword(data.Password),
      CreatedAt = DateTime.Now,
      IaActive = true
    };

    await _userRepository.Create(user);
  }
  
  public async Task<UserView?> GetUserViewAsync(string username)
  {
    var user = await _userRepository.GetByUsername(username);
    
    if (user == null)
      return null;
    
    return new UserView
    {
      Username = user.Username,
      Email = user.Email,
      Name = user.Name,
      Bio = user.Bio
    };
  }

  public async Task DeleteAccount(string username)
  {
    var user = await _userRepository.GetByUsername(username);
    
    await _userRepository.Delete(user!.Id.ToString());
  }
}