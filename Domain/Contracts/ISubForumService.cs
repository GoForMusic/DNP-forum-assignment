using Domain.Models;

namespace Domain.Contracts;

public interface ISubForumService
{
    public Task<ICollection<SubForum>> GetSubForumAsync();
    public Task<SubForum> GetSubForumByID(string id);
    public Task<SubForum> AddSubForum(SubForum subforum);
    public Task DeleteSubForum(string id);
    public Task UpdateSubForum(SubForum subforum);
}