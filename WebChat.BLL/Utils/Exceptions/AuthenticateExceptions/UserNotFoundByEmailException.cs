namespace WebChat.BLL.Utils.Exceptions.AuthenticateExceptions;

public class UserNotFoundByEmailException : AuthenticationException
{
  public UserNotFoundByEmailException(string email) 
    : base($"No user found with this email address: {email}")
  {
  }
}