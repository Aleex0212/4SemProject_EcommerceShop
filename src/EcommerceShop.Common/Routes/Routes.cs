namespace EcommerceShop.Common.Routes
{
  public static class Routes
  {
    public static class GatewayRoutes
    {
      public const string BaseUrl = "Gateway";

      public static class OrderGatewayRoutes
      {
        public const string Order = BaseUrl + "/Order";
      }
    }

    public static class OrderRoutes
    {
      public const string BaseUrl = "Order";
    }
    public static class ProductRoutes
    {
      public const string BaseUrl = "Product";
    }
  }
}
