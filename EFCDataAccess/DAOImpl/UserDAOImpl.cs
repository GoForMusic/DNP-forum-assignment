using System.Runtime.CompilerServices;
using Application.DAOInterfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCDataAccess.DAOImpl;

public class UserDAOImpl : IUserDAO
{
    private readonly DBContext _db;

    public UserDAOImpl(DBContext _db)
    {
        this._db = _db;
    }
    
    public async Task<ICollection<User>> GetListAsync()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<User> GetElementAsync(string id)
    {
        return await _db.Users.FirstAsync(t=>t.Id.Equals(id));
    }

    public async Task<User> AddElementAsync(User user)
    {
        try{
            EntityEntry<User> added = await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return added.Entity;
        }catch (Exception e)
        {
            Console.WriteLine(e+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    public async Task DeleteElementAsync(string id)
    {
        User? existing = await _db.Users.FindAsync(id);
        if (existing is null)
        {
            throw new Exception($"Could not find user with id {id}. Nothing was deleted");
        }
        _db.Users.Remove(existing);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateElementAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User> GetUser(string username)
    {
        try{
            return await _db.Users.FirstAsync(t => t.UserName.Equals(username));
        }catch (Exception e)
        {
            Console.WriteLine(e+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }
}