using System.Runtime.Serialization;

namespace WebChat.BLL.Utils.Exceptions.RegistrationExceptions;

[Serializable]
public class InvalidRegistrationDataException : RegistrationException
{
  public InvalidRegistrationDataException() 
    : base("Invalid user data provided during registration.")
  {
  }
  
  public InvalidRegistrationDataException(string message) 
    : base(message)
  {
  }

  public InvalidRegistrationDataException(string message, Exception innerException) 
    : base(message, innerException)
  {
  }

  protected InvalidRegistrationDataException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}