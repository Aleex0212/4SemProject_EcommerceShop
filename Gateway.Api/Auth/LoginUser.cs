using Dapr.Client;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;

namespace Gateway.Api.Auth
{
  public class LoginUser
  {
    private readonly ILogger<LoginUser> _logger;
    private readonly DaprClient _daprClient;
    private readonly TokenProvider _tokenProvider;

    public LoginUser(DaprClient daprClient, TokenProvider tokenProvider, ILogger<LoginUser> logger)
    {
      _daprClient = daprClient;
      _tokenProvider = tokenProvider;
      _logger = logger;
    }

    internal async Task<string> AuthLogin(LoginDto login)
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          "userservice-api",
          Routes.UserRoutes.Login,
          login);

        var responseJson = await _daprClient.InvokeMethodWithResponseAsync(request);
        responseJson.EnsureSuccessStatusCode();
        var user = await responseJson.Content.ReadFromJsonAsync<UserDto>();
        if (user is null) return $"user for login {login.Email} not found";
        var token = _tokenProvider.Create(user);

        return token;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"could not create token for login {login.Email}");
        return string.Empty;
      }
    }
  }
}
