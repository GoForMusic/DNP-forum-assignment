using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class VoteController : ControllerBase
{
    private IVoteService _voteService;


    public VoteController(IVoteService _voteService)
    {
        this._voteService = _voteService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<Vote>>> GetAll()
    {
        try
        {
            ICollection<Vote> votes = await _voteService.GetListAsync();
            return Ok(votes);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ICollection<Vote>>> GetByID([FromRoute] string id)
    {
        try
        {
            Vote vote = await _voteService.GetElementAsync(id);
            return Ok(vote);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<ICollection<Vote>>> DeleteByID([FromRoute] string id)
    {
        try
        {
            await _voteService.DeleteElementAsync(id);
            return Ok("Element deleted: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    //update
    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] Vote vote)
    {
        try
        {
            await _voteService.UpdateElementAsync(vote);
            return Ok("Element updated: " + vote.Id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }  
    }
    
    //create
    [HttpPost]
    public async Task<ActionResult<Vote>> AddVote([FromBody] Vote vote)
    {
        try
        {
            Vote added = await _voteService.AddElementAsync(vote);
            return Created($"/todos/{added.Id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}