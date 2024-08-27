using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
  
  [HttpPost("login")]
  public IActionResult Login()
  { 
    var token = GenerateJwtToken("oleg");
    return Ok(new { Token = token });
  }
  
  public string GenerateJwtToken(string username)
  {
    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, username),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var key = new SymmetricSecurityKey("123412341234d/hmiposjt'pjp'eojm'rhutlatemgkthaojg'l:mpgreiajg'aorgiohj1234"u8.ToArray());
    var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: "webChat",
      audience: "client",
      claims: claims,
      expires: DateTime.Now.AddMinutes(30),
      signingCredentials: creeds);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
  
  [Authorize]
  [HttpGet("get")]
  public IActionResult Get()
  {
    return Ok();
  }
}