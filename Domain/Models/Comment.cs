namespace Domain.Models;

public class Comment
{
    public string Id { get; set; }
    public string Body { get; set; }
    public ICollection<Vote> Votes { get; set; }
    public User WrittenBy { get; set; }
}