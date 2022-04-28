using System.Runtime.CompilerServices;
using Application.DAOInterfaces;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCDataAccess.DAOImpl;

public class CommentDAOImpl : ICommentDAO
{
    private readonly DBContext _db;

    public CommentDAOImpl(DBContext _db)
    {
        this._db = _db;
    }
    
    public async Task<ICollection<Comment>> GetListAsync()
    {
        try
        {
            //kinda mess????
            return await _db.Comments.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<Comment> GetElementAsync(string id)
    {
        try
        {
            //kinda mess????
            return await _db.Comments.FirstAsync(t=>t.Id.Equals(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task<Comment> AddElementAsync(Comment comment)
    {
        try
        {
            Comment? added = comment;
            added.WrittenBy = await _db.Users.FirstAsync(t=>t.Id.Equals(comment.WrittenBy.Id));
            added.Votes = new List<Vote>();
            await _db.Comments.AddAsync(added);
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
        try
        {
            Comment? existing = await _db.Comments.FindAsync(id);
            if (existing is null)
            {
                throw new Exception($"Could not find Todo with id {id}. Nothing was deleted");
            }

            _db.Votes.RemoveRange(existing.Votes);
            _db.Comments.Remove(existing);
            await _db.SaveChangesAsync();
        }catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task UpdateElementAsync(Comment comment)
    {
        try
        {
            Comment? localPost = comment;
            localPost.WrittenBy = await _db.Users.FirstAsync(t => t.Id.Equals(comment.WrittenBy.Id));
            localPost.Votes = await _db.Votes.Where(t => t.CommentId.Equals(comment.Id)).ToListAsync();
            _db.Comments.Update(comment);
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }
}