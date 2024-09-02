using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebChat.BLL.Interfaces;

namespace WebChat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
  private readonly IAccountService _accountServices;

  public AccountController(IAccountService accountServices)
  {
    _accountServices = accountServices;
  }
  
  [Authorize]
  [HttpGet]
  public async Task<IActionResult> GetUserView()
  {
    var username = HttpContext.User.Identity!.Name;

    var userView = await _accountServices.GetUserViewAsync(username!);

    if (userView == null)
    {
      return NotFound("User not found.");
    }
    
    return Ok(userView);
  }
  
  [Authorize]
  [HttpDelete]
  public async Task<IActionResult> DeleteAccount()
  {
    var username = HttpContext.User.Identity!.Name;
    
    await _accountServices.DeleteAccount(username!);
    
    return Ok();
  }
}