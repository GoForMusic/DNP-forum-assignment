﻿using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SubForumController : ControllerBase
{
    private ISubForumService _subForumServiceImpl;

    public SubForumController(ISubForumService _subForumServiceImpl)
    {
        this._subForumServiceImpl = _subForumServiceImpl;
    }

    //get all subforums
    [HttpGet]
    public async Task<ActionResult<ICollection<SubForum>>> GetAll()
    {
        try
        {
            ICollection<SubForum> subForums = await _subForumServiceImpl.GetListAsync();
            return Ok(subForums);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
    // public Task<SubForum> GetSubForumByID(string id);
    //public Task<SubForum> AddSubForum(SubForum subforum);
    //public Task DeleteSubForum(string id);
    //public Task UpdateSubForum(SubForum subforum);
    
    //get subforum by ID
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ICollection<SubForum>>> GetByID([FromRoute] string id,[FromQuery] string? title)
    {
        try
        {
            SubForum subForum = await _subForumServiceImpl.GetElementAsync(id);
            if (title != null)
            {
                //return a new list of elements base on the query
                SubForum localForum = subForum;
                localForum.Posts = subForum.Posts.Where(t=>t.Header.Contains(title)).ToList();
                
                return Ok(localForum);
            }
            else return Ok(subForum);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    //delete subforum by ID
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<ICollection<SubForum>>> DeleteByID([FromRoute] string id)
    {
        try
        {
            await _subForumServiceImpl.DeleteElementAsync(id);
            return Ok("Element deleted: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    //update
    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] SubForum subForum)
    {
        Console.WriteLine("???");
        try
        {
            await _subForumServiceImpl.UpdateElementAsync(subForum);
            Console.WriteLine(Ok("Element updated: " + subForum.Id));
            return Ok("Element updated: " + subForum.Id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }  
    }
    
    //create
    [HttpPost]
    public async Task<ActionResult<SubForum>> AddSubForum([FromBody] SubForum subForum)
    {
        try
        {
            SubForum added = await _subForumServiceImpl.AddElementAsync(subForum);
            return Created($"/todos/{added.Id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}