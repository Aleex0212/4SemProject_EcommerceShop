using Dapr.Client;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Api.Controllers
{
  [AllowAnonymous]
  [Route(Routes.GatewayRoutes.UserGatewayRoutes.User)]
  [ApiController]
  public class UserGateway : Controller
  {
    private readonly DaprClient _daprClient;
    private readonly ILogger<OrderGateway> _logger;

    public UserGateway(DaprClient daprClient, ILogger<OrderGateway> logger)
    {
      _daprClient = daprClient;
      _logger = logger;
    }

    [HttpGet("{email}")]
    [SwaggerOperation(
      Summary = "Get user by email",
      Description = "Retrieves a single user by their email address",
      Tags = new[] { "User" })]
    public async Task<IActionResult> Get(string email)
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          httpMethod: HttpMethod.Get,
          appId: "userservice-api",
          methodName: $"{Routes.UserRoutes.GetByEmail.Replace("{email}", email)}");
      
        var responseJson = await _daprClient.InvokeMethodWithResponseAsync(request);
        responseJson.EnsureSuccessStatusCode();

        var user = await responseJson.Content.ReadFromJsonAsync<UserDto>();

        if (user == null) return NotFound($"User with email {email} was not found");

        return Ok(user);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error retrieving user with email {email}: {ex.Message}");
        return StatusCode(500, "Internal server error while fetching the user");
      }
    }
  }
}
