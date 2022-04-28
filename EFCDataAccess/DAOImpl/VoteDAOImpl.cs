using Applicaiton.ServiceImpl;
using Application.DAOInterfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCDataAccess.DAOImpl;

public class VoteDAOImpl : IVoteDAO
{
    
    private readonly DBContext _db;

    public VoteDAOImpl(DBContext _db)
    {
        this._db = _db;
    }
    
    public async Task<ICollection<Vote>> GetListAsync()
    {
        try
        {
            //kinda mess????
            return await _db.Votes.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<Vote> GetElementAsync(string id)
    {
        try
        {
            //kinda mess????
            return await _db.Votes.FirstAsync(t=>t.Id.Equals(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<Vote> AddElementAsync(Vote vote)
    {
        try
        {
            Vote added = vote;
            added = vote;
            added.Voter = await _db.Users.FirstOrDefaultAsync(t=>t.Id.Equals(vote.Voter.Id));
            await _db.Votes.AddAsync(added);
            await _db.SaveChangesAsync();
            return added;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task DeleteElementAsync(string id)
    {
        Vote? existing = await _db.Votes.FindAsync(id);
        if (existing is null)
        {
            throw new Exception($"Could not find Todo with id {id}. Nothing was deleted");
        }
        _db.Votes.Remove(existing);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateElementAsync(Vote vote)
    {
        try
        {
            Vote? toUpdate = vote;
            toUpdate.Voter = await _db.Users.FirstAsync(t => t.Id.Equals(vote.Voter.Id));
            _db.Votes.Update(toUpdate);
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }
}