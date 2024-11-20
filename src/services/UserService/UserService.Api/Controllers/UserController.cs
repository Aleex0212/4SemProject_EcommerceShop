using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Db;

namespace UserService.Api.Controllers
{
  [AllowAnonymous]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly ILogger<UserController> _logger;
    private readonly UserData _userData;

    public UserController(UserData userData, ILogger<UserController> logger)
    {
      _userData = userData;
      _logger = logger;
    }

    [HttpPost(Routes.UserRoutes.Verify)]
    public IActionResult VerifyUser([FromBody] UserDto user)
    {
      try
      {
        var existingUser = _userData.Users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser is null) return NotFound($"User with Id: {user.Id} not found");
        return Ok($"user verification succeed for userId : {existingUser?.Id}");
      }
      catch (Exception)
      {
        _logger.LogError(500, $"Error verify user userId : {user.Id}");
        return StatusCode(500, $"Something went wrong during user verification userId : {user.Id}");
      }
    }

    [HttpPost(Routes.UserRoutes.Login)]
    public ActionResult<UserDto> Login([FromBody] LoginDto login)
    {
      try
      {
        var user = _userData.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.HashedPassword);
        if (user is null) return NotFound($"user not found for login : {login.Email}");
        return Ok(user);
      }
      catch (Exception)
      {
        _logger.LogError(500, $"error while attempting to login: {login.Email}");
        return StatusCode(500, $"error while attempting to login: {login.Email}");
      }
    }
  }
}
