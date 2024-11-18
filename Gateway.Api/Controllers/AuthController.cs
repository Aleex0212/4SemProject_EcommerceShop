using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Gateway.Api.Auth;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Api.Controllers
{
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly ILogger<AuthController> _logger;
    private readonly LoginUser _loginUser;

    public AuthController(LoginUser loginUser, ILogger<AuthController> logger)
    {
      _loginUser = loginUser;
      _logger = logger;
    }

    [HttpPost(Routes.GatewayRoutes.AuthRoutes.Login)]
    [SwaggerOperation(
      Summary = "User login",
      Description = "Endpoint for logging in a user and obtaining a JWT token",
      Tags = new[] { "Authentication" })]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
      try
      {
        var token = await _loginUser.AuthLogin(login);
        if (string.IsNullOrEmpty(token))
          return BadRequest($"Login attempt failed for user {login.Email}");

        return Ok(token);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Login failed for user {login.Email} , {login.Password}");
        return BadRequest($"Login attempt failed for user {login.Email}");
      }
    }
  }
}
