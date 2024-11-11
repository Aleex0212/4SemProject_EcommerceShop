namespace EcommerceShop.Common.Routes
{
  public static class Routes
  {
    public static class GatewayRoutes
    {
      public const string BaseUrl = "gateway";

      public static class OrderGatewayRoutes
      {
        public const string Order = BaseUrl + "/order";
      }
    }

    public static class OrderRoutes
    {
      public const string BaseUrl = "order";
    }
    public static class ProductRoutes
    {
      public const string BaseUrl = "product";
    }
    public static class PaymentRoutes
    {
      public const string BaseUrl = "payment";
    }
    public static class CustomerRoutes
    {
      public const string BaseUrl = "customer";
    }
  }
}
