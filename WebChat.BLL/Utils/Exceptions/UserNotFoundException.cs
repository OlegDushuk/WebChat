namespace WebChat.BLL.Utils.Exceptions;

public class UserNotFoundException : Exception
{
  public UserNotFoundException()
    : base("User not found for unknown reasons.")
  {
  }
  
  public UserNotFoundException(string email) 
    : base($"No user found with this email address: {email}")
  {
  }
}