using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebChat.DAL.Models;

public class User
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }
  
  [Required]
  [StringLength(100)]
  public string? Username { get; set; }
  
  [Required]
  [EmailAddress]
  [StringLength(100)]
  public string? Email { get; set; }
  
  [Required]
  [StringLength(256)]
  public string? PasswordHash { get; set; }
  
  public string? Name { get; set; }
  public string? Bio { get; set; }
  public DateTime CreatedAt { get; set; }
  public bool IaActive { get; set; }
}