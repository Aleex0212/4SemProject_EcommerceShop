using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Common.Queues
{
  public static class PubSub
  {
    public const string Base = "PubSub";
    public static class OrderTopic
    {
      public const string Base = "Order";
      public const string CreateOrder = "Create";

    }
    public static class ProductTopic
    {
      public const string Base = "Product";
      public const string Reserve = "Reserve";
    }
  }
}
