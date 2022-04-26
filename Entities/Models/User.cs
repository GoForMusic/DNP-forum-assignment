using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class User
{
    [Key] 
    public string Id { get; set; } = RandomIDGenerator.Generate(20);

    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }

    public User()
    {
        UserName = String.Empty;
        Password = String.Empty;
        Role = String.Empty;
        City= String.Empty;
        BirthDate = DateTime.Now;
    }
}