using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCDataAccess;

public class DBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<SubForum> SubForums { get; set; }
    public DbSet<Forum> Forums { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source = ..\EFCDataAccess\Forum.db");
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