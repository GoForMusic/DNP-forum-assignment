namespace Domain.Models;

public class User
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
    
}