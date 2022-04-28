

using Applicaiton.DAOInterfaces;
using Contracts;
using Entities.Models;

namespace Applicaiton.ServiceImpl;

public class UserServiceImpl : IUserService
{
    private IForumDAO _forumDao;

    public UserServiceImpl(IForumDAO _forumDao)
    {
        this._forumDao = _forumDao;
    }
    
    public async Task<ICollection<User>> GetListAsync()
    {
        return await _forumDao.GetUsersAsync();
    }

    public async Task<User> GetElementAsync(string id)
    {
        return await _forumDao.GetUserByID(id);
    }

    public async Task<User> GetUser(string username)
    {
        return await _forumDao.GetUser(username);
    }

    public async Task<User> AddElementAsync(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        return await _forumDao.AddUser(user);
    }

    public async Task DeleteElementAsync(string id)
    {
        await _forumDao.DeleteUser(id);
    }

    public async Task UpdateElementAsync(User user)
    {
        await _forumDao.UpdateUser(user);
    }
}