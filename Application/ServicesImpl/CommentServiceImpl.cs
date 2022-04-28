using Application.DAOInterfaces;
using Contracts;
using Entities.Models;

namespace Applicaiton.ServiceImpl;

public class CommentServiceImpl : ICommentService
{
    private ICommentDAO _dao ;

    public CommentServiceImpl(ICommentDAO _dao)
    {
        this._dao = _dao;
    }
    
    
    public async Task<ICollection<Comment>> GetListAsync()
    {
         return await _dao.GetListAsync();
    }

    public async Task<Comment> GetElementAsync(string id)
    {
        return await _dao.GetElementAsync(id);
    }

    public async Task<Comment> AddElementAsync(Comment comment)
    {
        return await _dao.AddElementAsync(comment);
    }

    public async Task DeleteElementAsync(string id)
    {
        await _dao.DeleteElementAsync(id);
    }

    public async Task UpdateElementAsync(Comment comment)
    {
        await _dao.UpdateElementAsync(comment);
    }
}