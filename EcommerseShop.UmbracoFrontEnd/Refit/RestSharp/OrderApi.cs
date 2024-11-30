using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using EcommerseShop.UmbracoFrontEnd.SessionStores;
using Newtonsoft.Json;
using RestSharp;

namespace EcommerseShop.UmbracoFrontEnd.Refit.RestSharp
{
  public class OrderApi
  {
    private readonly SessionStore _sessionStore;

    public OrderApi(SessionStore sessionStore)
    {
      _sessionStore = sessionStore;
    }

    public async Task CreateOrder(OrderDto order)
    {
      try
      {
        var options = new RestClientOptions("https://localhost:7136")
        {
          MaxTimeout = -1,
        };
        var client = new RestClient(options);
        var request = new RestRequest(Routes.GatewayRoutes.OrderGatewayRoutes.Order, Method.Post);
        request.AddHeader("Content-Type", "application/json");
        var token = _sessionStore.GetSessionToken();
        request.AddHeader("Authorization", token);
        var body = JsonConvert.SerializeObject(order);
        request.AddStringBody(body, DataFormat.Json);
        RestResponse response = await client.ExecuteAsync(request);
        Console.WriteLine(response.Content);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
  }
}
