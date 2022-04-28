using Application.DAOInterfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCDataAccess.DAOImpl;

public class SubForumDAOImpl : ISubForumDAO
{
    private readonly DBContext _db;

    public SubForumDAOImpl(DBContext _db)
    {
        this._db = _db;
    }
    
    public async Task<ICollection<SubForum>> GetListAsync()
    {
        try
        {
            //kinda mess????
            return await _db.SubForums.
                Include(f => f.OwnedBy).
                Include(f => f.Posts)
                .ThenInclude(f => f.WrittenBy).
                Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.WrittenBy).
                Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.Votes)
                .ThenInclude(f => f.Voter).
                Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.WrittenBy).
                Include(t=>t.Posts)
                .ThenInclude(t=>t.Votes)
                .ThenInclude(t=>t.Voter).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<SubForum> GetElementAsync(string id)
    {
        try
        {
            //Mess as well
            return await _db.SubForums.
                Include(f => f.OwnedBy).
                Include(f => f.Posts)
                .ThenInclude(f => f.WrittenBy).
                Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.WrittenBy).
                Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.Votes)
                .ThenInclude(f => f.Voter).
                Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.WrittenBy).
                Include(t=>t.Posts)
                .ThenInclude(t=>t.Votes)
                .ThenInclude(t=>t.Voter).FirstAsync(f=>f.Id.Equals(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<SubForum> AddElementAsync(SubForum subforum)
    {
        try
        {
            SubForum local = subforum;
            local.OwnedBy = await _db.Users.FirstAsync(t=>t.Id.Equals(subforum.OwnedBy.Id));
            await _db.SubForums.AddAsync(local);
            await _db.SaveChangesAsync();
            
            return subforum;
        }
        catch (Exception e)
        {
            Console.WriteLine(e+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task DeleteElementAsync(string id)
    {
        SubForum toDelete = _db.SubForums.First(t=>t.Id.Equals(id));
        if (toDelete is null)
        {
            throw new Exception($"Could not find sub-forum with id {id}. Nothing was deleted");
        }

        _db.SubForums.Remove(toDelete);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateElementAsync(SubForum subforum)
    {
        try
        {
            SubForum? localPost = subforum;
            localPost.OwnedBy =  await _db.Users.FirstAsync(t => t.Id.Equals(subforum.OwnedBy.Id));
            localPost.Posts = await _db.Posts.Where(t => t.SubForumId.Equals(subforum.Id)).ToListAsync();
            _db.SubForums.Update(localPost);
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<SubForum> GetSubForumByFilter(string id, string title)
    {
        throw new NotImplementedException();
    }
}