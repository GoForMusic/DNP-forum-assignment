using System.Text.Json;
using Domain.Models;

namespace FileData.DataAccess;

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
                Id = RandomIDGenerator.Generate(20),UserName = "admin", Password = "adrian1234", City = "Horsens", BirthDate = new DateTime(1998,12,26),Role = "Admin"
            },
            new User{
                Id = RandomIDGenerator.Generate(20), UserName = "user", Password = "adrian1234", City = "Horsens2", BirthDate = new DateTime(1998,12,26),Role = "User"
            },
            new User
            {
                Id = RandomIDGenerator.Generate(20), UserName = "adminSUB", Password = "adrian1234", City = "Horsens", BirthDate = new DateTime(199,12,26),Role = "SubForumAdmin"
            }
        };
            SubForum[] ts =
        {
            new SubForum
            {
                Id = RandomIDGenerator.Generate(20), Description = "Testing something about subForum", Title = "Gaming", Posts = new List<Post>(), OwnedBy = new User()
            },
            new SubForum()
            {
                Id = RandomIDGenerator.Generate(20), Description = "Gaming dot this", Title = "Testing second", Posts = new List<Post>(), OwnedBy = new User()
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