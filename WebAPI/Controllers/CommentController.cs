using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private ICommentService _service;


    public CommentController(ICommentService _service)
    {
        this._service = _service;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<Comment>>> GetAll()
    {
        try
        {
            ICollection<Comment> comments = await _service.GetListAsync();
            return Ok(comments);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ICollection<Comment>>> GetByID([FromRoute] string id)
    {
        try
        {
            Comment comment = await _service.GetElementAsync(id);
            return Ok(comment);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<ICollection<Comment>>> DeleteByID([FromRoute] string id)
    {
        try
        {
            await _service.DeleteElementAsync(id);
            return Ok("Element deleted: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    //update
    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] Comment comment)
    {
        try
        {
            await _service.UpdateElementAsync(comment);
            return Ok("Element updated: " + comment.Id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }  
    }
    
    //create
    [HttpPost]
    public async Task<ActionResult<Comment>> AddComment([FromBody] Comment comment)
    {
        try
        {
            Comment added = await _service.AddElementAsync(comment);
            return Created($"/todos/{added.Id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}