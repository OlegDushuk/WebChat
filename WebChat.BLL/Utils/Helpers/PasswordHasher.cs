using System.Security.Cryptography;
using System.Text;

namespace WebChat.BLL.Utils.Helpers;

public static class PasswordHasher
{
  public static string HashPassword(string password)
  {
    var passwordBytes = Encoding.UTF8.GetBytes(password);
    var hashBytes = SHA256.HashData(passwordBytes);
    return Convert.ToBase64String(hashBytes);
  }
  
  public static bool VerifyPassword(string hashedPassword, string providedPassword)
  {
    var providedPasswordHash = HashPassword(providedPassword);
    return hashedPassword == providedPasswordHash;
  }
}