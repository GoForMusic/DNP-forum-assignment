using Application.DAOInterfaces;
using Contracts;
using Entities.Models;

namespace Applicaiton.ServiceImpl;

public class PostServiceImpl : IPostSerivce
{
    private IPostDAO _dao ;

    public PostServiceImpl(IPostDAO _dao)
    {
        this._dao = _dao;
    }
    public async Task<ICollection<Post>> GetListAsync()
    {
        return await _dao.GetListAsync();
    }

    public async Task<Post> GetElementAsync(string id)
    {
        return await _dao.GetElementAsync(id);
    }

    public async Task<Post> AddElementAsync(Post element)
    {
        return await _dao.AddElementAsync(element);
    }

    public async Task DeleteElementAsync(string id)
    {
        await _dao.DeleteElementAsync(id);
    }

    public async Task UpdateElementAsync(Post element)
    {
        await _dao.UpdateElementAsync(element);
    }
}