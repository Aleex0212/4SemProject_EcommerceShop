using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Gateway.Api.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Api.Controllers
{
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly LoginCustomer _loginCustomer;

    public AuthController(LoginCustomer loginCustomer)
    {
      _loginCustomer = loginCustomer;
    }

    [HttpPost(Routes.GatewayRoutes.AuthRoutes.Login)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
      var token = await _loginCustomer.AuthLogin(loginDto);
      return Ok(token);
    }
  }
}
