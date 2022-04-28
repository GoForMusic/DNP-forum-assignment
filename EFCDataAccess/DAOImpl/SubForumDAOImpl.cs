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
            return await _db.SubForums.Include(f => f.OwnedBy).Include(f => f.Posts)
                .ThenInclude(f => f.WrittenBy).Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.WrittenBy).Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.Votes)
                .ThenInclude(f => f.Voter).Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.WrittenBy).ToListAsync();
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
            return await _db.SubForums.Include(f => f.OwnedBy).Include(f => f.Posts)
                .ThenInclude(f => f.WrittenBy).Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.WrittenBy).Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.Votes)
                .ThenInclude(f => f.Voter).Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.WrittenBy).FirstAsync(f=>f.Id==id);
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
        Console.WriteLine(subforum.Title + subforum.Posts.First().Id);
        Console.WriteLine("-0---");
        SubForum toUpdate = await GetElementAsync(subforum.Id);
        toUpdate.OwnedBy = await _db.Users.FindAsync(subforum.OwnedBy.Id);
        toUpdate.Posts = await _db.Posts.ToListAsync();
        toUpdate.Posts.Add(subforum.Posts.Single());
        _db.SubForums.Update(toUpdate);
        await _db.SaveChangesAsync();
    }

    public async Task<SubForum> GetSubForumByFilter(string id, string title)
    {
        throw new NotImplementedException();
    }
}