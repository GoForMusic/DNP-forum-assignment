

using Entities.Models;

namespace Contracts;

public interface IUserService
{
    public Task<ICollection<User>> GetUsersAsync();
    public Task<User> GetUserByID(string id);
    public Task<User> GetUser(string username);
    public Task<User> AddUser(User user);
    public Task DeleteUser(string id);
    public Task UpdateUser(User user);
}