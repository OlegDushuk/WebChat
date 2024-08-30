using WebChat.DAL.Models;

namespace WebChat.BLL.Models.DTOs;

public class UserDto
{
  public string? Username { get; set; }
  public string? Email { get; set; }
  public string? Name { get; set; }
  public bool IsActive { get; set; }
}