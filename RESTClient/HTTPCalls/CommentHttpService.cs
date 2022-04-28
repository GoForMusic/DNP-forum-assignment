using System.Text.Json;
using Contracts;
using Entities.Models;

namespace RESTClient;

public class CommentHttpService : ICommentService
{
    public async Task<ICollection<Comment>> GetListAsync()
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Get,"/comment");
        
            ICollection<Comment> comments = JsonSerializer.Deserialize<ICollection<Comment>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return comments;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Comment> GetElementAsync(string id)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Get,$"/comment/{id}");
        
            Comment comment = JsonSerializer.Deserialize<Comment>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return comment;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<Comment> AddElementAsync(Comment commentBody)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Post,"/comment", commentBody);
        
            Comment comment = JsonSerializer.Deserialize<Comment>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return comment;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task DeleteElementAsync(string id)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Delete,$"/comment/{id}");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task UpdateElementAsync(Comment comment)
    {
        try
        {
            string content =  await ServerAPI.getContent(Methods.Patch,"/comment", comment);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}