namespace Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public Comment ParentComment { get; set; }
    public ICollection<Vote> Votes { get; set; }
    public User WrittenBy { get; set; }
}