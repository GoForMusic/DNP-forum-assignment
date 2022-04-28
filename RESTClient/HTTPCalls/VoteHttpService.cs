using System.Text.Json;
using Contracts;
using Entities.Models;

namespace RESTClient;

public class VoteHttpService : IVoteService
{
    public async Task<ICollection<Vote>> GetListAsync()
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Get,"/vote");
        
            ICollection<Vote> votes = JsonSerializer.Deserialize<ICollection<Vote>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return votes;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Vote> GetElementAsync(string id)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Get,$"/vote/{id}");
        
            Vote vote = JsonSerializer.Deserialize<Vote>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return vote;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<Vote> AddElementAsync(Vote vote)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Post,"/vote", vote);
        
            Vote voteLocal = JsonSerializer.Deserialize<Vote>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return voteLocal;
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
            string content = await ServerAPI.getContent(Methods.Delete,$"/vote/{id}");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task UpdateElementAsync(Vote vote)
    {
        try
        {
            string content =  await ServerAPI.getContent(Methods.Patch,"/vote", vote);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}