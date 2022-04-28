using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Vote
{
    [Key] public string Id { get; set; } = RandomIDGenerator.Generate(20);
    public User Voter { get; set; }
    [ForeignKey("Comment")]
    public string? CommentId { get; set; }
    [ForeignKey("Post")]
    public string? PostId { get; set; }

    public Vote()
    {
        Voter = new User();
    }
}