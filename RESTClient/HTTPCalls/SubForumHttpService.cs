using System.Text.Json;
using Contracts;
using Entities.Models;

namespace RESTClient;

public class SubForumHttpService : ISubForumService
{
    public async Task<ICollection<SubForum>> GetListAsync()
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Get,"/subforum");
        
            ICollection<SubForum> subForums = JsonSerializer.Deserialize<ICollection<SubForum>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return subForums;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<SubForum> GetElementAsync(string id)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Get,$"/subforum/{id}");
        
            SubForum subForum = JsonSerializer.Deserialize<SubForum>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return subForum;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<SubForum> GetSubForumByFilter(string id, string title)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Get,$"/subforum/{id}?title={title}");
        
            SubForum subForum = JsonSerializer.Deserialize<SubForum>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return subForum;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<SubForum> AddElementAsync(SubForum subforum)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Post,"/subforum", subforum);
        
            SubForum localSubForum = JsonSerializer.Deserialize<SubForum>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return localSubForum;
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
            await ServerAPI.getContent(Methods.Delete,$"/subforum/{id}");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task UpdateElementAsync(SubForum subforum)
    {
        try
        {
            await ServerAPI.getContent(Methods.Patch,"/subforum", subforum);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}