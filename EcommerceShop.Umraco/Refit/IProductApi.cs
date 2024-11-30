using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace EcommerceShop.Umraco.Refit
{
  public interface IProductApi
  {

    [Get(Routes.GatewayRoutes.ProductGatewayRoutes.Product)]
    Task<IEnumerable<ProductDto>> GetProducts([Header("Authorization")] string bearerToken);
  }
}
