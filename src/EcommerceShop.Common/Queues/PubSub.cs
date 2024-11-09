namespace EcommerceShop.Common.Queues
{
  public static class PubSub
  {
    public const string Channel = "pubsub";
    public static class OrderTopic
    {
      public const string Base = "order";
      public const string CreateOrder = "create";

    }
    public static class ProductTopic
    {
      public const string Base = "product";
      public const string Reserve = "reserve";
    }
  }
}
