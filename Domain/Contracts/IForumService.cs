using Domain.Models;

namespace Domain.Contracts;

public interface IForumService
{
    public Task<ICollection<Forum>> GetForumAsync();
    //TODO need to see what else we need from model
}