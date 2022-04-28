

using Entities.Models;

namespace Contracts;

public interface IUserService : IGenericService<User,string>
{
    public Task<User> GetUser(string username);
}