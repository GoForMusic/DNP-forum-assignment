using Applicaiton.DAOInterfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCDataAccess.DAOImpl;


public class ForumDAOImpl : IForumDAO
{
    private DBContext _dbContext;

    public ForumDAOImpl(DBContext _dbContext)
    {
        this._dbContext = _dbContext;
    }


    public async Task<ICollection<SubForum>> GetSubForumAsync()
    {
        try
        {
            //kinda mess????
            return await _dbContext.SubForums.Include(f => f.OwnedBy).Include(f => f.Posts)
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
            Console.WriteLine(e+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<SubForum> GetSubForumByID(string id)
    {
        try
        {
            //Mess as well
            return await _dbContext.SubForums.Include(f => f.OwnedBy).Include(f => f.Posts)
                .ThenInclude(f => f.WrittenBy).Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.WrittenBy).Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.Votes)
                .ThenInclude(f => f.Voter).Include(f => f.Posts)
                .ThenInclude(f => f.Comments)
                .ThenInclude(f => f.WrittenBy).FirstOrDefaultAsync(f=>f.Id==id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<SubForum> AddSubForum(SubForum subforum)
    {
        try
        {
            await _dbContext.SubForums.AddAsync(subforum);
            await _dbContext.SaveChangesAsync();
            return subforum;
        }
        catch (Exception e)
        {
            Console.WriteLine(e+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }


    public async Task DeleteSubForum(string id)
    {
        SubForum? toDelete = _dbContext.SubForums.First(t=>t.Id.Equals(id));
        if (toDelete is null)
        {
            throw new Exception($"Could not find sub-forum with id {id}. Nothing was deleted");
        }

        _dbContext.SubForums.Remove(toDelete);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateSubForum(SubForum subforum)
    {
        _dbContext.SubForums.Update(subforum);
        _dbContext.Entry(subforum).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<User>> GetUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User> GetUserByID(string id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User> GetUser(string username)
    {
        return _dbContext.Users.First(t => t.UserName.Equals(username));
    }

    public async Task<User> AddUser(User user)
    {
        EntityEntry<User> added = await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return added.Entity;
    }

    public async Task DeleteUser(string id)
    {
        User? existing = await _dbContext.Users.FindAsync(id);
        if (existing is null)
        {
            throw new Exception($"Could not find user with id {id}. Nothing was deleted");
        }
        _dbContext.Users.Remove(existing);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        _dbContext.SaveChangesAsync();
    }
}