using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace Ecommerce.FrontEnd.Interfaces.Refit
{
  public interface IProductApi
  {
    [Get(Routes.GatewayRoutes.ProductGatewayRoutes.Product)]
    Task<IEnumerable<ProductDto>?> GetProductsAsync();
  }
}
