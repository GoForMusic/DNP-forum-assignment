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
                Id= 0, UserName = "admin", Password = "adrian1234", City = "Horsens", BirthDate = new DateTime(1998,12,26),Role = "Admin", SecurityLevel = 2
            },
            new User{
                Id= 1, UserName = "user", Password = "adrian1234", City = "Horsens2", BirthDate = new DateTime(1998,12,26),Role = "User", SecurityLevel = 1
            }
        };
            SubForum[] ts =
        {
            new SubForum
            {
                Id=0, Description = "Testing something about subForum", Title = "Test"
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