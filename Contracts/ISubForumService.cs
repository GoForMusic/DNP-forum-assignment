

using Entities.Models;

namespace Contracts;

public interface ISubForumService : IGenericService<SubForum,string>
{
    public Task<SubForum> GetSubForumByFilter(string id, string title);
}