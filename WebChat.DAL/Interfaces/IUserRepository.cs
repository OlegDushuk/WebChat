using WebChat.DAL.Models;

namespace WebChat.DAL.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
  Task<User?> GetByUserId(string userId);
  Task<User?> GetByEmail(string email);
}