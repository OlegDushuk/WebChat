using System.Runtime.Serialization;

namespace WebChat.BLL.Utils.Exceptions.RegistrationExceptions;

public class UsernameAlreadyTakenException : RegistrationException
{
  public UsernameAlreadyTakenException(string username) 
    : base(FormatMessage(username))
  {
  }

  public UsernameAlreadyTakenException(string username, Exception innerException) 
    : base(FormatMessage(username), innerException)
  {
  }

  protected UsernameAlreadyTakenException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
  
  private static string FormatMessage(string username)
  {
    return $"The username '{username}' is already taken.";
  }
}