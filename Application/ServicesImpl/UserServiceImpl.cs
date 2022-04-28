using Application.DAOInterfaces;
using Contracts;
using Entities.Models;

namespace Applicaiton.ServiceImpl;

public class UserServiceImpl : IUserService
{
    private IUserDAO _dao;

    public UserServiceImpl(IUserDAO _dao)
    {
        this._dao = _dao;
    }
    
    public async Task<ICollection<User>> GetListAsync()
    {
        return await _dao.GetListAsync();
    }

    public async Task<User> GetElementAsync(string id)
    {
        return await _dao.GetElementAsync(id);
    }

    public async Task<User> GetUser(string username)
    {
        return await _dao.GetUser(username);
    }

    public async Task<User> AddElementAsync(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        return await _dao.AddElementAsync(user);
    }

    public async Task DeleteElementAsync(string id)
    {
        await _dao.DeleteElementAsync(id);
    }

    public async Task UpdateElementAsync(User user)
    {
        await _dao.UpdateElementAsync(user);
    }
}