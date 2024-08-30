using Microsoft.AspNetCore.Mvc;
using WebChat.BLL.Interfaces;
using WebChat.BLL.Models;
using WebChat.BLL.Utils.Exceptions;
using WebChat.BLL.Utils.Exceptions.AuthenticateExceptions;
using WebChat.BLL.Utils.Exceptions.RegistrationExceptions;

namespace WebChat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IAccountService _accountServices;
  private readonly IAuthenticationService _authenticationServices;

  public AuthController(IAccountService accountServices, IAuthenticationService authenticationServices)
  {
    _accountServices = accountServices;
    _authenticationServices = authenticationServices;
  }
  
  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] UserRegistrationData data)
  {
    try
    {
      await _accountServices.RegisterUserAsync(data);
      return Ok();
    }
    catch (InvalidDataException e)
    {
      return BadRequest(e.Message);
    }
    catch (Exception e) when (e is EmailAlreadyInUseException or UsernameAlreadyTakenException)
    {
      return Conflict(e.Message);
    }
    catch (RegistrationException e)
    {
      return StatusCode(500, new {e.Message});
    }
    catch (Exception e)
    {
      return StatusCode(500, new {e.Message});
    }
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] UserLoginData data, string audience)
  {
    try
    {
      var user = await _authenticationServices.AuthenticateUserAsync(data);
      var token = _authenticationServices.GetJwtToken(user, audience);
      return Ok(new { token });
    }
    catch (InvalidDataException e)
    {
      return BadRequest(e.Message);
    }
    catch (UserNotFoundException e)
    {
      return NotFound(e.Message);
    }
    catch (AuthenticationException e)
    {
      return BadRequest(e.Message);
    }
    catch (Exception e)
    {
      return StatusCode(500, new {e.Message});
    }
  }
}