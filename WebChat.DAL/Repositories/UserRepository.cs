using Microsoft.EntityFrameworkCore;
using WebChat.DAL.Interfaces;
using WebChat.DAL.Models;

namespace WebChat.DAL.Repositories;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext _db;

  public UserRepository(AppDbContext db)
  {
    _db = db;
  }

  public async Task<User?> Get(string id)
  {
    return await _db.Users.FindAsync(id);
  }

  public async Task<List<User>> GetAll()
  {
    return await _db.Users.ToListAsync();
  }

  public async Task Create(User entity)
  {
    _db.Users.Add(entity);
    await _db.SaveChangesAsync();
  }

  public async Task Update(string id, User entity)
  {
    var existingUser = await _db.Users.FindAsync(id);
    if (existingUser == null)
    {
      throw new KeyNotFoundException("User not found");
    }
    
    existingUser.Username = entity.Username;
    existingUser.Email = entity.Email;
    existingUser.PasswordHash = entity.PasswordHash;
    existingUser.Name = entity.Name;
    existingUser.Bio = entity.Bio;
    existingUser.CreatedAt = entity.CreatedAt;
    existingUser.IaActive = entity.IaActive;

    _db.Users.Update(existingUser);
    await _db.SaveChangesAsync();
  }

  public async Task Delete(string id)
  {
    var user = await _db.Users.FindAsync(id);
    if (user == null)
    {
      throw new KeyNotFoundException("User not found");
    }

    _db.Users.Remove(user);
    await _db.SaveChangesAsync();
  }

  public async Task<User?> GetByUsername(string username)
  {
    return await _db.Users
      .SingleOrDefaultAsync(u => u.Username == username);
  }

  public async Task<User?> GetByEmail(string email)
  {
    return await _db.Users
      .SingleOrDefaultAsync(u => u.Email == email);
  }
}