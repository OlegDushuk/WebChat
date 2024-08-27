using WebChat.BLL.Interfaces;
using WebChat.BLL.Models;
using WebChat.BLL.Utils.Exceptions.RegistrationExceptions;
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
      throw new InvalidRegistrationDataException("Data is null or empty.");
    
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
      PasswordHash = data.Password,
      CreatedAt = DateTime.Now
    };

    await _userRepository.Create(user);
  }
}