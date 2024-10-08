﻿using Microsoft.EntityFrameworkCore;
using WebChat.DAL.Models;

namespace WebChat.DAL;

public class AppDbContext : DbContext
{
  public DbSet<User> Users { get; set; }
  
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    
    modelBuilder.Entity<User>()
      .HasIndex(u => u.Username)
      .IsUnique();
    
    modelBuilder.Entity<User>()
      .HasIndex(u => u.Email)
      .IsUnique();
  }
}
