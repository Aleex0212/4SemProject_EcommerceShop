using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserService.Api.Db;

namespace UserService.Api.Controllers
{
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
    [SwaggerOperation(
      Summary = "Verify a user",
      Description = "Endpoint for verify customer is found with user",
      Tags = new[] { "User" })]
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
    [SwaggerOperation(
      Summary = "Login",
      Description = "Endpoint for checking is user and customer credentials is correct",
      Tags = new[] { "User" })]
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

    [HttpGet(Routes.UserRoutes.GetByEmail)]
    [SwaggerOperation(
      Summary = "Get a user by email",
      Description = "Endpoint for retrieving a user based on the provided email",
      Tags = new[] { "User" })]
    public IActionResult GetUserByEmail(string email)
    {
      try
      {
        if (string.IsNullOrEmpty(email))
          return BadRequest("Email is required.");

        var existingUser = _userData.Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (existingUser is null)
          return NotFound($"User with email: {email} not found");

        return Ok(existingUser);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Error retrieving user with email: {email}");
        return StatusCode(500, "An unexpected error occurred while retrieving the user.");
      }
    }
  }
}
