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
            
            SubForum localSubForum = await GetSubForumByID(subforum.Id);
            localSubForum.Description = subforum.Description;
            localSubForum.Title = subforum.Title;
            localSubForum.OwnedBy = await GetUserByID(subforum.OwnedBy.Id);
            localSubForum.Posts = await _dbContext.Posts.ToListAsync();
            
            //post
            foreach (var item in subforum.Posts)
            {
                Post? localPost = await _dbContext.Posts.FirstOrDefaultAsync(t => t.Id.Equals(item.Id));
                if (localPost == null)
                {
                    
                }
                else
                {
                    Console.WriteLine(item.Header+"_____2");
                    //post details
                    localPost.date_posted = item.date_posted;
                    localPost.Body = item.Body;
                    localPost.Header = item.Header;
                    localPost.WrittenBy = await GetUserByID(item.WrittenBy.Id);
                    //comments details
                    foreach (var item2 in item.Comments)
                    {
                        Comment? localComment =
                            await _dbContext.Comments.FirstOrDefaultAsync(t => t.Id.Equals(item2.Id));
                        if (localComment == null)
                        {
                            _dbContext.Comments.Add(item2);
                        }
                        else
                        {
                            localComment.Body = item2.Body;
                            localComment.WrittenBy = await GetUserByID(item2.WrittenBy.Id);
                            //vote
                            foreach (var item3 in item2.Votes)
                            {
                                Vote? localVote = await _dbContext.Votes.FirstOrDefaultAsync(t => t.Id.Equals(item3.Id));
                                if (localVote == null)
                                {
                                    _dbContext.Votes.Add(item3);
                                }
                                else
                                {
                                    localVote.Voter = await GetUserByID(item3.Voter.Id);
                                }
                            }
                            
                        }
                    }
                    //vote details
                    foreach (var item2 in item.Votes)
                    {
                        Vote? localVote = await _dbContext.Votes.FirstOrDefaultAsync(t => t.Id.Equals(item2.Id));
                        if (localVote == null)
                        {
                            _dbContext.Votes.Add(item2);
                        }
                        else
                        {
                            localVote.Voter = await GetUserByID(item2.Voter.Id);
                        }
                    }
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