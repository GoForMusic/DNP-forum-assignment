using Applicaiton.DAOInterfaces;
using Entities.Models;
using JsonDataAccess.JsonContext;

namespace JsonDataAccess.DAOImpl;

public class ForumDAOImpl : IForumDAO
{
    private FileContext _fileContext;

    public ForumDAOImpl(FileContext fileContext)
    {
        _fileContext = fileContext;
    }


    public async Task<ICollection<SubForum>> GetSubForumAsync()
    {
        return _fileContext.Forum.SubForums;
    }

    public async Task<SubForum> GetSubForumByID(string id)
    {
        return _fileContext.Forum.SubForums.First(t=>t.Id.Equals(id));
    }

    public async Task<SubForum> AddSubForum(SubForum subforum)
    {
        _fileContext.Forum.SubForums.Add(subforum);
        _fileContext.SaveChanges();
        return subforum;
    }

    public async Task DeleteSubForum(string id)
    {
        SubForum toDelete = _fileContext.Forum.SubForums.First(t => t.Id.Equals(id));
        _fileContext.Forum.SubForums.Remove(toDelete);
        _fileContext.SaveChanges();
    }

    public async Task UpdateSubForum(SubForum subforum)
    {
        SubForum toUpdate = _fileContext.Forum.SubForums.First(t => t.Id.Equals(subforum.Id));
        toUpdate.Posts = subforum.Posts;
        toUpdate.Description = subforum.Description;
        toUpdate.Title = subforum.Title;
        toUpdate.OwnedBy = subforum.OwnedBy;
        _fileContext.SaveChanges();
    }

    public async Task<ICollection<User>> GetUsersAsync()
    {
        return _fileContext.Forum.Users;
    }

    public async Task<User> GetUserByID(string id)
    {
        return _fileContext.Forum.Users.First(t=>t.Id.Equals(id));
    }

    public async Task<User> GetUser(string username)
    {
        return _fileContext.Forum.Users.First(t=>t.UserName.Equals(username));
    }

    public async Task<User> AddUser(User user)
    {
        if (_fileContext.Forum.Users.Any(t=>t.UserName.Equals(user.UserName)))
        {
            throw new Exception("Username already in use!");
        }
        else
        {
            
            _fileContext.Forum.Users.Add(user);
            _fileContext.SaveChanges();
            return user;
        }
        
    }

    public async Task DeleteUser(string id)
    {
        User toDelete = _fileContext.Forum.Users.First(t => t.Id.Equals(id));
        _fileContext.Forum.Users.Remove(toDelete);
        _fileContext.SaveChanges();
    }

    public async Task UpdateUser(User user)
    {
        User toUpdate = _fileContext.Forum.Users.First(t => t.Id.Equals(user.Id));
        toUpdate.UserName = user.UserName;
        toUpdate.Password = user.Password;
        toUpdate.Role = user.Role;
        toUpdate.BirthDate = user.BirthDate;
        toUpdate.City = user.City;
        _fileContext.SaveChanges();
    }
}