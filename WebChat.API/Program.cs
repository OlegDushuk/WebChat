using Microsoft.EntityFrameworkCore;
using WebChat.DAL;
using WebChat.DAL.Interfaces;
using WebChat.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Add repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();