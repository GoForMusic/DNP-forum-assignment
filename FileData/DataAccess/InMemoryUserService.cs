using Domain.Contracts;
using Domain.Models;

namespace FileData.DataAccess;

public class InMemoryUserService : IUserService
{
    public Task<User?> GetUserAsync(string username)
    {
        User? find = users.Find(user => user.UserName.Equals(username));
        return Task.FromResult(find);
    }

    private List<User> users = new()
    {
        new User
        {
            UserName = "adrian", Password = "adrian1234", City = "Horsens", BirthDate = new DateTime(1998,12,26),Role = "Admin", SecurityLevel = 2
        },
        new User{
            UserName = "adrian2", Password = "adrian1234", City = "Horsens2", BirthDate = new DateTime(1998,12,26),Role = "User", SecurityLevel = 1
        }
    };
}