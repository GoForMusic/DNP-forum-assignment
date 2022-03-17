namespace Domain.Models;

public class Vote
{
    public string Id { get; set; }
    public short Value { get; set; }
    public User Voter { get; set; }

    public Vote(short value)
    {
        this.Value = value;
    }

    private void Validate(short value)
    {
        
    }
    
}