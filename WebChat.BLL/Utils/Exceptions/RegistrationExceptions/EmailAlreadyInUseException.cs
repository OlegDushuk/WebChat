using System.Runtime.Serialization;

namespace WebChat.BLL.Utils.Exceptions.RegistrationExceptions;

[Serializable]
public class EmailAlreadyInUseException : RegistrationException
{
  public EmailAlreadyInUseException(string email) 
    : base(FormatMessage(email))
  {
  }

  public EmailAlreadyInUseException(string email, Exception innerException) 
    : base(FormatMessage(email), innerException)
  {
  }

  protected EmailAlreadyInUseException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
  
  private static string FormatMessage(string email)
  {
    return $"The email '{email}' is already in use.";
  }
}