

using Applicaiton.DAOInterfaces;
using Contracts;
using Entities.Models;

namespace Applicaiton.ServiceImpl;

public class SubForumServiceImpl : ISubForumService
{
    private IForumDAO _forumDao;

    public SubForumServiceImpl(IForumDAO _forumDao)
    {
        this._forumDao = _forumDao;
    }
    
    public async Task<ICollection<SubForum>> GetSubForumAsync()
    {
        return await _forumDao.GetSubForumAsync();
    }

    public async Task<SubForum> GetSubForumByID(string id)
    {
        return await _forumDao.GetSubForumByID(id);
    }

    public async Task<SubForum> GetSubForumByFilter(string id, string title)
    {
        return await _forumDao.GetSubForumByID(id);
    }

    public async Task<SubForum> AddSubForum(SubForum subforum)
    {
        return await _forumDao.AddSubForum(subforum);
    }

    public async Task DeleteSubForum(string id)
    {
        await _forumDao.DeleteSubForum(id);
    }

    public async Task UpdateSubForum(SubForum subforum)
    {
        await _forumDao.UpdateSubForum(subforum);
    }
}