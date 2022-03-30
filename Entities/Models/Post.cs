namespace Entities.Models;

public class Post
{
    public string Id { get; set; }
    public string Header { get; set; }
    public string Body { get; set; }
    public List<Vote> Votes { get; set; }
    public List<Comment> Comments { get; set; }
    public User WrittenBy { get; set; }
    public DateTime date_posted { get; set; }

    public Post()
    {
        Comments = new List<Comment>();
        Id = RandomIDGenerator.Generate(20);
        Header=String.Empty;
        Body=String.Empty;
        Votes = new List<Vote>();
        WrittenBy = new User();
        date_posted = new DateTime();
    }
}