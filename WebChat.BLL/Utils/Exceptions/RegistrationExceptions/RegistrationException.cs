using System.Runtime.Serialization;

namespace WebChat.BLL.Utils.Exceptions.RegistrationExceptions;

[Serializable]
public class RegistrationException : Exception
{
  public RegistrationException()
    : base("An unknown exception occurred during registration.")
  {
  }

  public RegistrationException(string message) 
    : base(FormatMessage(message))
  {
  }

  public RegistrationException(string message, Exception innerException)
    : base(FormatMessage(message), innerException)
  {
  }

  protected RegistrationException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  private static string FormatMessage(string message)
  {
    return $"An exception occurred during registration: {message}";
  }
}