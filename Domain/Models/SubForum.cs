namespace Domain.Models;

public class SubForum
{
    public string Id { get; set; }
    public User OwnedBy { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<Post> Posts { get; set; }
}