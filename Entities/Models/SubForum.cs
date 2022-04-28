using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class SubForum
{
    [Key] public string Id { get; set; } = RandomIDGenerator.Generate(20);
    public User OwnedBy { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<Post>? Posts { get; set; }

    public SubForum()
    {
        OwnedBy = new User();
        Title= String.Empty;
        Description = String.Empty;
        Posts = new List<Post>();
    }
}