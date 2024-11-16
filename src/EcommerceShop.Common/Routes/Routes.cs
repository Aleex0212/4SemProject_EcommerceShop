namespace EcommerceShop.Common.Routes
{
  public static class Routes
  {
    public static class GatewayRoutes
    {
      public const string BaseUrl = "gateway";

      public static class AuthRoutes
      {
        public const string Auth = "auth";
        public const string Login = Auth + "/login";
      }

      public static class OrderGatewayRoutes
      {
        public const string Order = BaseUrl + "/order";
      }
    }

    public static class OrderRoutes
    {
      public const string BaseUrl = "order";
      public const string Create = BaseUrl + "/create";
      public const string Update = BaseUrl + "/update";
      public const string Delete = BaseUrl + "/delete";
    }
    public static class ProductRoutes
    {
      public const string BaseUrl = "product";
      public const string Release = BaseUrl + "/release";
      public const string Reserve = BaseUrl + "/reserve";
    }
    public static class PaymentRoutes
    {
      public const string BaseUrl = "payment";
    }
    public static class CustomerRoutes
    {
      public const string BaseUrl = "customer";
      public const string Login = BaseUrl + "/login";
    }
  }
}
