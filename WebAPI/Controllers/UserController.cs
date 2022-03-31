using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService _userService)
    {
        this._userService = _userService;
    }

    //get all users
    [HttpGet]
    public async Task<ActionResult<ICollection<User>>> GetAll()
    {
        try
        {
            ICollection<User> users = await _userService.GetUsersAsync();
            return Ok(users);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    //get user by ID
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ICollection<User>>> GetByID([FromRoute] string id)
    {
        try
        {
            User user = await _userService.GetUserByID(id);
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    //get user by ID
    [HttpGet]
    [Route("username/{username}")]
    public async Task<ActionResult<ICollection<User>>> GetUserByUsername([FromRoute] string username)
    {
        try
        {
            User user = await _userService.GetUser(username);
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    //delete user by ID
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<ICollection<User>>> DeleteByID([FromRoute] string id)
    {
        try
        {
            await _userService.DeleteUser(id);
            return Ok("Element deleted: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    //update
    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] User user)
    {
        try
        {
            await _userService.UpdateUser(user);
            return Ok("Element updated: " + user.Id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }  
    }
    
    //create
    [HttpPost]
    public async Task<ActionResult<User>> AddUser([FromBody] User user)
    {
        try
        {
            User added = await _userService.AddUser(user);
            return Created($"/todos/{added.Id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}