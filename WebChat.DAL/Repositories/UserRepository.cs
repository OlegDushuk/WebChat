using Microsoft.EntityFrameworkCore;
using WebChat.DAL.Interfaces;
using WebChat.DAL.Models;

namespace WebChat.DAL.Repositories;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext _appDbContext;

  public UserRepository(AppDbContext appDbContext)
  {
    _appDbContext = appDbContext;
  }

  public async Task<User?> Get(string id)
  {
    return await _appDbContext.Users.FindAsync(id);
  }

  public async Task<List<User>> GetAll()
  {
    return await _appDbContext.Users.ToListAsync();
  }

  public async Task Create(User entity)
  {
    _appDbContext.Users.Add(entity);
    await _appDbContext.SaveChangesAsync();
  }

  public async Task Update(string id, User entity)
  {
    var existingUser = await _appDbContext.Users.FindAsync(id);
    if (existingUser == null)
    {
      throw new KeyNotFoundException("User not found");
    }
    
    existingUser.UserId = entity.UserId;
    existingUser.Email = entity.Email;
    existingUser.PasswordHash = entity.PasswordHash;
    existingUser.Name = entity.Name;
    existingUser.Bio = entity.Bio;
    existingUser.CreatedAt = entity.CreatedAt;
    existingUser.IaActive = entity.IaActive;

    _appDbContext.Users.Update(existingUser);
    await _appDbContext.SaveChangesAsync();
  }

  public async Task Delete(string id)
  {
    var user = await _appDbContext.Users.FindAsync(id);
    if (user == null)
    {
      throw new KeyNotFoundException("User not found");
    }

    _appDbContext.Users.Remove(user);
    await _appDbContext.SaveChangesAsync();
  }

  public async Task<User?> GetByUserId(string userId)
  {
    return await _appDbContext.Users
      .SingleOrDefaultAsync(u => u.UserId == userId);
  }

  public async Task<User?> GetByEmail(string email)
  {
    return await _appDbContext.Users
      .SingleOrDefaultAsync(u => u.Email == email);
  }
}