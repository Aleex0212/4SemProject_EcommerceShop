using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Enum;
using EcommerseShop.UmbracoFrontEnd.Refit;
using EcommerseShop.UmbracoFrontEnd.Refit.RestSharp;
using EcommerseShop.UmbracoFrontEnd.SessionStores;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace EcommerseShop.UmbracoFrontEnd.Controllers
{
  public class BasketController : SurfaceController
  {
    private readonly OrderApi orderApi1;
    private readonly SessionStore _sessionStore;
    private readonly IOrderApi _orderApi;
    private readonly Basket _basket;
    private readonly LoggedInUser _loggedInUser;
    public BasketController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
      ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger,
      IPublishedUrlProvider publishedUrlProvider, Basket basket, LoggedInUser loggedInUser, IOrderApi orderApi,
      SessionStore sessionStore, OrderApi orderApi1)
      : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
      _basket = basket;
      _loggedInUser = loggedInUser;
      _orderApi = orderApi;
      _sessionStore = sessionStore;
      this.orderApi1 = orderApi1;
    }

    [HttpPost]
    public IActionResult AddProduct(ProductDto product)
    {
      _basket.Products.First(x => x.Product.Id == product.Id).Quantity++;
      return CurrentUmbracoPage();
    }

    [HttpPost]
    public IActionResult SubtractProduct(ProductDto product)
    {
      _basket.Products.First(x => x.Product.Id == product.Id).Quantity--;

      var productLine = _basket.Products.First(x => x.Product.Id == product.Id);
      if (productLine.Quantity < 1)
      {
        _basket.Products.Remove(productLine);
      }

      return CurrentUmbracoPage();
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(string redirectUrl)
    {
      try
      {
        var prices = _basket.Products.Select(x => x.Product.Price * x.Quantity);
        var order = new OrderDto()
        {
          Id = Guid.NewGuid(),
          ProductLines = _basket.Products,
          Customer = _loggedInUser.User,
          Status = OrderStatus.Received,
          TotalPrice = prices.Sum()

        };
        var token = _sessionStore.GetSessionToken();

        await orderApi1.CreateOrder(order);

        //await _orderApi.CreateOrderAsync(order, token);

        return Redirect(redirectUrl);
      }
      catch (Exception debug)
      {
        return CurrentUmbracoPage();
      }
    }
  }
}
