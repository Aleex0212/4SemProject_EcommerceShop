namespace EcommerceShop.Common.Routes
{
  public static class Routes
  {
    public static class GatewayRoutes
    {
      public const string BaseUrl = "/gateway";

      public static class AuthRoutes
      {
        public const string Auth = "/auth";
        public const string Login = Auth + "/login";
      }
      public static class OrderGatewayRoutes
      {
        public const string Order = BaseUrl + "/order";
      }
      public static class ProductGatewayRoutes
      {
        public const string Product = BaseUrl + "/product";
      }
      public static class PaymmentGatewayRoutes
      {
        public const string Payment = BaseUrl + "/payment";
      }
      public static class UserGatewayRoutes
      {
        public const string User = BaseUrl + "/user";
      }
    }

    public static class OrderRoutes
    {
      public const string BaseUrl = "order";
      public const string Create = BaseUrl + "/create";
      public const string Update = BaseUrl + "/update";
      public const string Delete = BaseUrl + "/delete";
      public const string Get = BaseUrl + "/get";
      public const string GetById = BaseUrl + "/get/{id}";
    }
    public static class ProductRoutes
    {
      public const string BaseUrl = "product";
      public const string Release = BaseUrl + "/release";
      public const string Reserve = BaseUrl + "/reserve";
      public const string Create = BaseUrl + "/create";
      public const string Get = BaseUrl + "/get";
      public const string GetById = BaseUrl + "/get/{id}";
      public const string Update = BaseUrl + "/update";
    }

    public static class PaymentRoutes
    {
      public const string Payment = "payment";
    }
    public static class UserRoutes
    {
      public const string BaseUrl = "user";
      public const string Verify = BaseUrl + "/verify";
      public const string Login = BaseUrl + "/login";
      public const string GetByEmail = BaseUrl + "/getByEmail/{email}";
    }
  }
}
