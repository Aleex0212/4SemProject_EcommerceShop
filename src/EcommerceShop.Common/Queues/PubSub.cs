namespace EcommerceShop.Common.Queues
{
  public static class PubSub
  {
    public const string Channel = "pubsub";
    public static class OrderTopic
    {
      public const string Base = "order";
      public const string CreateOrder = "create";
      public const string UpdateOrder = "update";
      public const string DeleteOrder = "delete";
    }
    public static class ProductTopic
    {
      public const string Base = "product";
      public const string Create = "create";
      public const string Update = "update";
      public const string Reserve = "reserve";
    }

    public static class PaymentTopic
    {
      public const string Base = "payment";
      public const string Create = "create";
    }
  }
}
