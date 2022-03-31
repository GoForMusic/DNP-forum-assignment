

using Entities.Models;

namespace Contracts;

public interface ISubForumService
{
    public Task<ICollection<SubForum>> GetSubForumAsync();
    public Task<SubForum> GetSubForumByID(string id);
    public Task<SubForum> GetSubForumByFilter(string id, string title);
    public Task<SubForum> AddSubForum(SubForum subforum);
    public Task DeleteSubForum(string id);
    public Task UpdateSubForum(SubForum subforum);
}