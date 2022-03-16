using Domain.Models;

namespace Domain.Contracts;

public interface ISubForumService
{
    public Task<ICollection<SubForum>> GetSubForumAsync();
    public Task<SubForum> GetSubForumByID(int id);
    public Task<SubForum> AddSubForum(SubForum subforum);
    public Task DeleteSubForum(int id);
    public Task UpdateSubForum(SubForum subforum);
}