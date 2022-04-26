using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Post
{
    [Key] public string Id { get; set; } = RandomIDGenerator.Generate(20);
    [Required]
    public string Header { get; set; }
    [Required]
    public string Body { get; set; }
    public ICollection<Vote>? Votes { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public User? WrittenBy { get; set; }
    public DateTime? date_posted { get; set; }

    public Post()
    {
        Header=String.Empty;
        Body = String.Empty;
        Votes = new List<Vote>();
        Comments = new List<Comment>();
        WrittenBy = new User();
        date_posted = DateTime.Now;
    }
}