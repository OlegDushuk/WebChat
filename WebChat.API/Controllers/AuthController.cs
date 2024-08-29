using Microsoft.AspNetCore.Mvc;
using WebChat.BLL.Interfaces;
using WebChat.BLL.Models;
using WebChat.BLL.Utils.Exceptions.RegistrationExceptions;

namespace WebChat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IAccountService _accountServices;

  public AuthController(IAccountService accountServices)
  {
    _accountServices = accountServices;
  }
  
  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] UserRegistrationData data)
  {
    try
    {
      await _accountServices.RegisterUserAsync(data);
      return Ok();
    }
    catch (InvalidRegistrationDataException e)
    {
      return BadRequest(e.Message);
    }
    catch (Exception e) when (e is EmailAlreadyInUseException or UsernameAlreadyTakenException)
    {
      return Conflict(e.Message);
    }
    catch (RegistrationException e)
    {
      return StatusCode(500, e.Message);
    }
    catch (Exception e)
    {
      return StatusCode(500, e.Message);
    }
  }
}