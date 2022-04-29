using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private IPostSerivce _serivce;


    public PostController(IPostSerivce _serivce)
    {
        this._serivce = _serivce;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<Post>>> GetAll()
    {
        try
        {
            ICollection<Post> votes = await _serivce.GetListAsync();
            return Ok(votes);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ICollection<Post>>> GetByID([FromRoute] string id)
    {
        try
        {
            Post vote = await _serivce.GetElementAsync(id);
            return Ok(vote);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<ICollection<Post>>> DeleteById([FromRoute] string id)
    {
        try
        {
            await _serivce.DeleteElementAsync(id);
            return Ok("Element deleted: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    //update
    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] Post post)
    {
        try
        {
            await _serivce.UpdateElementAsync(post);
            return Ok("Element updated: " + post.Id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }  
    }
    
    //create
    [HttpPost]
    public async Task<ActionResult<Post>> AddSubForum([FromBody] Post post)
    {
        try
        {
            Post added = await _serivce.AddElementAsync(post);
            return Created($"/todos/{added.Id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}