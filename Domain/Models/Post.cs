namespace Domain.Models;

public class Post
{
    public string Id { get; set; }
    public string Header { get; set; }
    public string Body { get; set; }
    public ICollection<Vote> Votes { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public User WrittenBy { get; set; }
    public DateTime date_posted { get; set; }
}