using WebChat.DAL.Models;

namespace WebChat.DAL.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
  Task<User?> GetByUsername(string username);
  Task<User?> GetByEmail(string email);
}