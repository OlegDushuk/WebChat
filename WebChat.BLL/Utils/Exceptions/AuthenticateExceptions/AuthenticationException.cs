using System.Runtime.Serialization;

namespace WebChat.BLL.Utils.Exceptions.AuthenticateExceptions;

public class AuthenticationException : Exception
{
  public AuthenticationException()
    : base("An unknown exception occurred during authentication.")
  {
  }
  
  public AuthenticationException(string message) 
    : base(message)
  {
  }

  public AuthenticationException(string message, Exception innerException)
    : base(message, innerException)
  {
  }

  protected AuthenticationException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}