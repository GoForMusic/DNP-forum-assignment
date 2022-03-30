namespace Entities.Models;

public class Vote
{
    public string Id { get; set; }
    public User Voter { get; set; }

    public Vote()
    {
        Id= RandomIDGenerator.Generate(20);
        Voter = new User();
    }
}