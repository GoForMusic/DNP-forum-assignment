﻿using System.Text.Json;
using Contracts;
using Entities.Models;

namespace RESTClient;

public class UserHttpService : IUserService
{
    public async Task<ICollection<User>> GetListAsync()
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Get,"/user");
        
            ICollection<User> user = JsonSerializer.Deserialize<ICollection<User>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return user;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<User> GetElementAsync(string id)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Get,$"/user/{id}");
        
            User user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return user;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<User> GetUser(string username)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Get,$"/user/username/{username}");
        
            User user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return user;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<User> AddElementAsync(User user)
    {
        try
        {
            string content = await ServerAPI.getContent(Methods.Post,"/user", user);
        
            User userlocal = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return userlocal;
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
            string content = await ServerAPI.getContent(Methods.Delete,$"/user/{id}");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task UpdateElementAsync(User user)
    {
        try
        {
            string content =  await ServerAPI.getContent(Methods.Patch,"/user", user);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}