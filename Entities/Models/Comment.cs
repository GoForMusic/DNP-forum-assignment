namespace Entities.Models;

public class Comment
{
    public string Id { get; set; }
    public string Body { get; set; }
    public List<Vote> Votes { get; set; }
    public User WrittenBy { get; set; }

    public Comment()
    {
        Id = RandomIDGenerator.Generate(20);
        Body = String.Empty;
        Votes = new List<Vote>();
        WrittenBy = new User();
    }
}