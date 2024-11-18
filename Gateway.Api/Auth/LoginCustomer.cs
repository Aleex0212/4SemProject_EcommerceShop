using Dapr.Client;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;

namespace Gateway.Api.Auth
{
  public class LoginCustomer
  {
    private readonly DaprClient _daprClient;
    private readonly TokenProvider _tokenProvider;

    public LoginCustomer(DaprClient daprClient, TokenProvider tokenProvider)
    {
      _daprClient = daprClient;
      _tokenProvider = tokenProvider;
    }
    
    public async Task<string> AuthLogin(LoginDto login)
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          "userservice-api",
          Routes.CustomerRoutes.Login,
          login);

        var responseJson = await _daprClient.InvokeMethodWithResponseAsync(request);
        responseJson.EnsureSuccessStatusCode();
        var user = await responseJson.Content.ReadFromJsonAsync<UserDto>();
        var token = _tokenProvider.Create(user);
        
        return token;
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
    }
  }
}
