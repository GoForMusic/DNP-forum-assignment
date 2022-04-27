using System.Collections;
using Applicaiton.DAOInterfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SQLitePCL;

namespace EFCDataAccess.DAOImpl;


public class ForumDAOImpl : IForumDAO
{
    private readonly DBContext _dbContext;

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
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
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
              .ThenInclude(f => f.WrittenBy).FirstAsync(f=>f.Id==id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<SubForum> AddSubForum(SubForum subforum)
    {
        try
        {
            User? user = await _dbContext.Users.FirstAsync(t=>t.Id.Equals(subforum.OwnedBy.Id));
            SubForum local = subforum;
            local.OwnedBy = user;
            await _dbContext.SubForums.AddAsync(local);
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
        SubForum toDelete = _dbContext.SubForums.First(t=>t.Id.Equals(id));
        if (toDelete is null)
        {
            throw new Exception($"Could not find sub-forum with id {id}. Nothing was deleted");
        }

        _dbContext.SubForums.Remove(toDelete);
        await _dbContext.SaveChangesAsync();
    }

   
    public async Task UpdateSubForum(SubForum subforum)
    {
        try
        {


            Forum forum = await _dbContext.Forums.FirstAsync();
            forum.SubForums = await GetSubForumAsync();
            forum.Users = await GetUsersAsync();
            foreach (var item in forum.SubForums.Where(t=>t.Id.Equals(subforum.Id)))
            {
                _dbContext.Posts.UpdateRange(subforum.Posts);
            }
            
            
            foreach (var item in forum.SubForums.Where(t=>t.Id.Equals(subforum.Id)).First().Posts)
            {
                Console.WriteLine(item.Id + " " + item.Header);
                Console.WriteLine(item.WrittenBy.Id);
                Console.WriteLine(item.Votes.Count);
                foreach (var item2 in item.Comments)
                {
                    Console.WriteLine(item2.Id + " " + item2.Body);
                    Console.WriteLine(item2.WrittenBy.Id);
                    Console.WriteLine(item2.Votes.Count);
                }
            }


            
            //_dbContext.Forums.Update(forum);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<ICollection<User>> GetUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User> GetUserByID(string id)
    {
        try
        {
            return await _dbContext.Users.FirstAsync(t => t.Id.Equals(id));
        }catch (Exception e)
        {
            Console.WriteLine(e+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<User> GetUser(string username)
    {
        try{
            return await _dbContext.Users.FirstAsync(t => t.UserName.Equals(username));
        }catch (Exception e)
        {
            Console.WriteLine(e+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<User> AddUser(User user)
    {
        try{
            EntityEntry<User> added = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return added.Entity;
        }catch (Exception e)
        {
            Console.WriteLine(e+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
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
        await _dbContext.SaveChangesAsync();
    }
}