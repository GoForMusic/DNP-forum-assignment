namespace Domain.Models;

public class Vote
{
    public string Id { get; set; }
    public short Value { get; set; }
    public User Voter { get; set; }

    public Vote()
    {
        Id= RandomIDGenerator.Generate(20);
        Value = 0;
        Voter = new User();
    }
}