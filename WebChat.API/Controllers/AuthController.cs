using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebChat.BLL.Interfaces;
using WebChat.BLL.Models;
using WebChat.BLL.Utils.Exceptions;

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
  public IActionResult Register()
  {
    return Ok();
  }
}