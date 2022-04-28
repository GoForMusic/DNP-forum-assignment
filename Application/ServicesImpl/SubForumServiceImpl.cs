using Application.DAOInterfaces;
using Contracts;
using Entities.Models;

namespace Applicaiton.ServiceImpl;

public class SubForumServiceImpl : ISubForumService
{
    private ISubForumDAO _dao ;

    public SubForumServiceImpl(ISubForumDAO _dao)
    {
        this._dao = _dao;
    }
    
    public async Task<ICollection<SubForum>> GetListAsync()
    {
        return await _dao.GetListAsync();
    }

    public async Task<SubForum> GetElementAsync(string id)
    {
        return await _dao.GetElementAsync(id);
    }

    public async Task<SubForum> GetSubForumByFilter(string id, string title)
    {
        return await _dao.GetSubForumByFilter(id,title);
    }

    public async Task<SubForum> AddElementAsync(SubForum subforum)
    {
        return await _dao.AddElementAsync(subforum);
    }

    public async Task DeleteElementAsync(string id)
    {
        await _dao.DeleteElementAsync(id);
    }

    public async Task UpdateElementAsync(SubForum subforum)
    {
        await _dao.UpdateElementAsync(subforum);
    }
}