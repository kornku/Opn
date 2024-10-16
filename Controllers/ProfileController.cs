using Microsoft.AspNetCore.Mvc;
using Opn.Infrastructures;
using Opn.Interfaces;

namespace Opn.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController: ControllerBase
{
    private readonly IUserService _service;

    public ProfileController(IUserService service)
    {
        _service = service;
    }
    //get profile
    [HttpGet("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        try
        {
            var user = _service.GetProfile(id);

            return Ok(user);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }  
        catch (Exception e)
        {
            return StatusCode(500);
        }      
    }
    
    //edit profile
    [HttpPut("{id:guid}")]
    public IActionResult Edit([FromRoute] Guid id,[FromBody]UpdateUserRequest request)
    {
        try
        {
            var user = _service.UpdateProfile(id,request);

            return Ok(user);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }  
        catch (Exception e)
        {
            return StatusCode(500);
        }        
    }
    
    public record UpdateUserRequest(Gender Gender, string Dob, string Address, bool IsSubscribe);

}