namespace WebChat.BLL.Utils.Exceptions.AuthenticateExceptions;

public class InvalidPasswordException : AuthenticationException
{
  public InvalidPasswordException() : base("Invalid password.")
  {
    
  }
}