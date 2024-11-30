using EcommerceShop.Common.Dto;

namespace EcommerseShop.UmbracoFrontEnd.SessionStores
{
  public class Basket
  {
    public List<ProductLineDto> Products { get;} = new List<ProductLineDto>();
  }
}
