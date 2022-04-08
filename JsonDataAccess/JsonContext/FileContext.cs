using System.Text.Json;
using Entities.Models;

namespace JsonDataAccess.JsonContext;

public class FileContext
{
    private string forumFilePath = "forum.json";

    private Forum? forum;

    public Forum Forum
    {
        get
        {
            if (forum == null)
            {
                LoadData();
            }

            return forum!;
        }
    }

    public FileContext()
    {
        if (!File.Exists(forumFilePath))
        {
            Seed();
        }
    }

    private void Seed()
    {
        forum = new Forum();
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
            SubForum[] ts =
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


            forum.Users = users.ToList();
            forum.SubForums = ts.ToList();
        SaveChanges();
    }

    public void SaveChanges()
    {
        string serialize = JsonSerializer.Serialize(Forum);
        File.WriteAllText(forumFilePath, serialize);
        forum = null;
    }

    private void LoadData()
    {
        string content = File.ReadAllText(forumFilePath);
        forum = JsonSerializer.Deserialize<Forum>(content);
    }
}