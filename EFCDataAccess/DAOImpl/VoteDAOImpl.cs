using Application.DAOInterfaces;
using Entities.Models;

namespace EFCDataAccess.DAOImpl;

public class VoteDAOImpl : IVoteDAO
{
    
    private readonly DBContext _db;

    public VoteDAOImpl(DBContext _db)
    {
        this._db = _db;
    }
    
    public Task<ICollection<Vote>> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Vote> GetElementAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Vote> AddElementAsync(Vote subforum)
    {
        throw new NotImplementedException();
    }

    public Task DeleteElementAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateElementAsync(Vote subforum)
    {
        throw new NotImplementedException();
    }
}