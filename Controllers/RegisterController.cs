using Microsoft.AspNetCore.Mvc;
using Opn.Infrastructures;
using Opn.Interfaces;

namespace Opn.Controllers;

[ApiController]
[Route("[controller]")]
public class RegisterController: ControllerBase
{
    private readonly IRegisterService _service;

    public RegisterController(IRegisterService service)
    {
        _service = service;
    }
    
    //register
    [HttpPost()]
    public IActionResult Register([FromBody]RegisterRequest request)
    {
        try
        {
            var user = _service.Register(request);

            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest();
        }       
    }
    
    //get token to change password
    [HttpGet("{id:guid}/token")]
    public IActionResult GetToken([FromRoute] Guid id)
    {
        try
        {
            var user = _service.GetToken(id);

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
    
    //change password
    [HttpPost("{id:guid}/change_password")]
    public IActionResult ChangePassword([FromRoute] Guid id,ChangePasswordRequest request)
    {
        try
        {
            var user = _service.ChangePassword(id,request.Password,request.Token);

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
    
    public record RegisterRequest(string Email, string Name, string Password, string Dob, string Address, bool IsSubscribe,
        Gender Gender);

    public record ChangePasswordRequest(string Password, string Token);
}