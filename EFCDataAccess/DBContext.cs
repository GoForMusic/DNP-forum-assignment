using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCDataAccess;

public class DBContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Vote> Votes { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<SubForum> SubForums { get; set; } = null!;
    public DbSet<Forum> Forums { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source = ..\EFCDataAccess\Forum.db");
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public void Seed()
    {
        if(SubForums.Any()) return;
        if(Users.Any()) return;
        
        User[] users =
        {
            new User
            {
                UserName = "admin", Password = BCrypt.Net.BCrypt.HashPassword("adrian1234"), City = "Horsens", BirthDate = new DateTime(1998,12,26),Role = "Admin"
            },
            new User{
                UserName = "user", Password = BCrypt.Net.BCrypt.HashPassword("adrian1234"), City = "Horsens2", BirthDate = new DateTime(1998,12,26),Role = "User"
            },
            new User
            {
                UserName = "adminSUB", Password = BCrypt.Net.BCrypt.HashPassword("adrian1234"), City = "Horsens", BirthDate = new DateTime(199,12,26),Role = "SubForumAdmin"
            }
        };
        SubForum[] subForums =
        {
            new SubForum
            {
                Description = "Testing something about subForum", Title = "Gaming"
            },
            new SubForum()
            {
                Description = "Gaming dot this", Title = "Testing second"
            }
        };



        Forum forum = new Forum();
        forum.Users = users;
        forum.SubForums = subForums;
        Forums.AddRange(forum);
        SaveChanges();
    }
    
    
}