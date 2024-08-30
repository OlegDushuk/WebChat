using System.Runtime.Serialization;

namespace WebChat.BLL.Utils.Exceptions.AuthenticateExceptions;

public class AuthenticationException : Exception
{
  public AuthenticationException()
    : base("An unknown exception occurred during authentication.")
  {
  }
  
  public AuthenticationException(string message) 
    : base(FormatMessage(message))
  {
  }

  public AuthenticationException(string message, Exception innerException)
    : base(FormatMessage(message), innerException)
  {
  }

  protected AuthenticationException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
  
  private static string FormatMessage(string message)
  {
    return $"An exception occurred during authentication: {message}";
  }
}