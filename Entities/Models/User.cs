using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class User
{
    [Key] 
    public string Id { get; set; } = RandomIDGenerator.Generate(20);
    public string UserName { get; set; }
    public string Password { get; set; }
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