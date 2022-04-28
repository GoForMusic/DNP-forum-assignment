using Application.DAOInterfaces;
using Contracts;
using Entities.Models;

namespace Applicaiton.ServiceImpl;

public class VoteServiceImpl : IVoteService
{
    private IVoteDAO _dao ;

    public VoteServiceImpl(IVoteDAO _dao)
    {
        this._dao = _dao;
    }
    public async Task<ICollection<Vote>> GetListAsync()
    {
        return await _dao.GetListAsync();
    }

    public async Task<Vote> GetElementAsync(string id)
    {
        return await _dao.GetElementAsync(id);
    }

    public async Task<Vote> AddElementAsync(Vote element)
    {
        return await _dao.AddElementAsync(element);
    }

    public async Task DeleteElementAsync(string id)
    {
        await _dao.DeleteElementAsync(id);
    }

    public async Task UpdateElementAsync(Vote element)
    {
        await _dao.UpdateElementAsync(element);
    }
}