using Domain.Models;

namespace Domain.Contracts;

public interface IUserService
{
    public Task<User?> GetUserAsync(string username);
}