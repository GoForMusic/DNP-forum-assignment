

using Entities.Models;

namespace Applicaiton.DAOInterfaces;

public interface IForumDAO
{
    public Task<ICollection<SubForum>> GetSubForumAsync();
    public Task<SubForum> GetSubForumByID(string id);
    public Task<SubForum> AddSubForum(SubForum subforum);
    public Task DeleteSubForum(string id);
    public Task UpdateSubForum(SubForum subforum);
    public Task<ICollection<User>> GetUsersAsync();
    public Task<User> GetUserByID(string id);
    public Task<User> GetUser(string username);
    public Task<User> AddUser(User user);
    public Task DeleteUser(string id);
    public Task UpdateUser(User user);
}