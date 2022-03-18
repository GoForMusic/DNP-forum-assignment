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
    
    
    public bool Valid()
    {
        if (Value == 1) return true;
        else
        {
            return false;
        }
    }
    
    public bool canVote(User user)
    {
        if (Voter.Equals(user)) return false;
        else
        {
            Value = 1;
            return true;
        }
    }
    
}