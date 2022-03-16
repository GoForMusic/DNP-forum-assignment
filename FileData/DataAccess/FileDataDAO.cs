using Domain.Contracts;
using Domain.Models;

namespace FileData.DataAccess;

public class FileDataDAO : ISubForumService, IUserService
{
    private FileContext _fileContext;

    public FileDataDAO(FileContext fileContext)
    {
        _fileContext = fileContext;
    }


    public async Task<ICollection<SubForum>> GetSubForumAsync()
    {
        return _fileContext.Forum.SubForums;
    }

    public async Task<SubForum> GetSubForumByID(int id)
    {
        return _fileContext.Forum.SubForums.First(t=>t.Id==id);
    }

    public async Task<SubForum> AddSubForum(SubForum subforum)
    {
        int largestId = _fileContext.Forum.SubForums.Max(t => t.Id);
        int nextId = largestId + 1;
        subforum.Id = nextId;
        _fileContext.Forum.SubForums.Add(subforum);
        _fileContext.SaveChanges();
        return subforum;
    }

    public async Task DeleteSubForum(int id)
    {
        SubForum toDelete = _fileContext.Forum.SubForums.First(t => t.Id == id);
        _fileContext.Forum.SubForums.Remove(toDelete);
        _fileContext.SaveChanges();
    }

    public async Task UpdateSubForum(SubForum subforum)
    {
        SubForum toUpdate = _fileContext.Forum.SubForums.First(t => t.Id == subforum.Id);
        toUpdate.Description = subforum.Description;
        toUpdate.Title = subforum.Title;
        toUpdate.OwnedBy = subforum.OwnedBy;
        _fileContext.SaveChanges();
    }

    public async Task<ICollection<User>> GetUsersAsync()
    {
        return _fileContext.Forum.Users;
    }

    public async Task<User> GetUserByID(int id)
    {
        return _fileContext.Forum.Users.First(t=>t.Id==id);
    }

    public async Task<User> GetUser(string username)
    {
        return _fileContext.Forum.Users.First(t=>t.UserName.Equals(username));
    }

    public async Task<User> AddUser(User user)
    {
        int largestId = _fileContext.Forum.Users.Max(t => t.Id);
        int nextId = largestId + 1;
        user.Id = nextId;
        _fileContext.Forum.Users.Add(user);
        _fileContext.SaveChanges();
        return user;
    }

    public async Task DeleteUser(int id)
    {
        User toDelete = _fileContext.Forum.Users.First(t => t.Id == id);
        _fileContext.Forum.Users.Remove(toDelete);
        _fileContext.SaveChanges();
    }

    public async Task UpdateUser(User user)
    {
        User toUpdate = _fileContext.Forum.Users.First(t => t.Id == user.Id);
        toUpdate.UserName = user.UserName;
        toUpdate.Password = user.Password;
        toUpdate.Role = user.Role;
        toUpdate.BirthDate = user.BirthDate;
        toUpdate.City = user.City;
        toUpdate.SecurityLevel = user.SecurityLevel;
        _fileContext.SaveChanges();
    }
}