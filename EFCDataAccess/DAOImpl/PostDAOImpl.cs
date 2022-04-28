using System.Runtime.CompilerServices;
using Application.DAOInterfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCDataAccess.DAOImpl;

public class PostDAOImpl: IPostDAO
{
    private readonly DBContext _db;

    public PostDAOImpl(DBContext _db)
    {
        this._db = _db;
    }
    public async Task<ICollection<Post>> GetListAsync()
    {
        try
        {
            //kinda mess????
            return await _db.Posts.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<Post> GetElementAsync(string id)
    {
        try
        {
            //kinda mess????
            return await _db.Posts.FirstAsync(t=>t.Id.Equals(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<Post> AddElementAsync(Post post)
    {
        try
        {
            Post? added = post;
            added.WrittenBy = await _db.Users.FirstAsync(t=>t.Id.Equals(post.WrittenBy.Id));
            added.Comments = new List<Comment>();
            added.Votes = new List<Vote>();
            await _db.Posts.AddAsync(added);
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
        try{
            Post? existing = await _db.Posts.FindAsync(id);
            if (existing is null)
            {
                throw new Exception($"Could not find Todo with id {id}. Nothing was deleted");
            }

            
            _db.Votes.RemoveRange(existing.Votes);
            _db.Comments.RemoveRange(existing.Comments);
            _db.Posts.Remove(existing);
            await _db.SaveChangesAsync();
        }catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task UpdateElementAsync(Post post)
    {
        try
        {
            Post? localPost = post;
            localPost.Comments = await _db.Comments.Where(t => t.PostId.Equals(post.Id)).ToListAsync();
            localPost.WrittenBy = await _db.Users.FirstAsync(t => t.Id.Equals(post.WrittenBy.Id));
            localPost.Votes = await _db.Votes.Where(t => t.PostId.Equals(post.Id)).ToListAsync();
            _db.Posts.Update(post);
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }
}