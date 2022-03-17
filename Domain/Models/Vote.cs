namespace Domain.Models;

public class Vote
{
    public string? Id { get; set; }
    public short? Value { get; set; }
    public User? Voter { get; set; }

    public Vote()
    {
        Id= String.Empty;
        Value = 0;
        Voter = new User();
    }
    public Vote(short value)
    {
        this.Value = value;
    }

    private void Validate(short value)
    {
        
    }
    
}