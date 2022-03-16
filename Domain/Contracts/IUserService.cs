using Domain.Models;

namespace Domain.Contracts;

public interface IUserService
{
    public Task<ICollection<User>> GetUsersAsync();
    public Task<User> GetUserByID(int id);
    public Task<User> GetUser(string username);
    public Task<User> AddUser(User user);
    public Task DeleteUser(int id);
    public Task UpdateUser(User user);
}