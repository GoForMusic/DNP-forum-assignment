using System.Text.Json;
using Contracts;
using Entities.Models;

namespace RESTClient;

public class PostHttpService : IPostSerivce
{
    public async Task<ICollection<Post>> GetListAsync()
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Get,"/post");
        
            ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return posts;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Post> GetElementAsync(string id)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Get,$"/post/{id}");
        
            Post post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return post;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<Post> AddElementAsync(Post post)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Post,"/post", post);
        
            Post localPost = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return localPost;
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
            string content = await ServerAPI.getContent(Methods.Delete,$"/post/{id}");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task UpdateElementAsync(Post post)
    {
        try
        {
            string content =  await ServerAPI.getContent(Methods.Patch,"/post", post);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}