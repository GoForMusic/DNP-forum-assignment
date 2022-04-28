using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Comment
{
    [Key] 
    public string Id { get; set; } = RandomIDGenerator.Generate(20);
    [Required]
    public string Body { get; set; }
    public ICollection<Vote>? Votes { get; set; }
    public User WrittenBy { get; set; }
    [ForeignKey("Post")]
    public string PostId { get; set; }

    public Comment()
    {
        Body = String.Empty;
        Votes = new List<Vote>();
        WrittenBy = new User();
    }
}