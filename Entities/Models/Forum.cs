using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Forum
{
    [Key] public string Id { get; set; } = RandomIDGenerator.Generate(20);
    public ICollection<SubForum>? SubForums { get; set; }
    public ICollection<User>? Users { get; set; }

    public Forum()
    {
        SubForums = new List<SubForum>();
        Users = new List<User>();
    }
}