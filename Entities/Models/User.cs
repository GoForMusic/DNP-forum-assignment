namespace Entities.Models;

public class User
{
    public string Id { get; set; }
    public string UserName { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }

    public User()
    {
        Id = RandomIDGenerator.Generate(20);
        UserName=String.Empty;
        Password=String.Empty;
        Role=String.Empty;
        City=String.Empty;
        BirthDate = new DateTime();
    }
    
}