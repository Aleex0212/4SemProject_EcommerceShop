using EcommerceShop.Common.Enum;
using OrderService.Domain.Models;

namespace OrderService.Persistence.Db
{
  public class OrderData
  {
    private ProductData Products;
    private CustomerData Customers;
    public List<Order> Orders { get; }

    public OrderData(ProductData products, CustomerData customers)
    {
      Products = products;
      Customers = customers;
      Orders = new List<Order>();

      var productLines1 = new List<ProductLine>
      {
        ProductLine.Create(Guid.NewGuid(), Products.Products[0], 2),
        ProductLine.Create(Guid.NewGuid(), Products.Products[1], 2)
      };
      Orders.Add(Order.Create(
        new Guid("11111111-1111-1111-1111-111111111111"),
        Customers.Customers[0],
        productLines1,
        OrderStatus.Completed));

      var productLines2 = new List<ProductLine>
      {
        ProductLine.Create(Guid.NewGuid(), Products.Products[2], 2),
        ProductLine.Create(Guid.NewGuid(), Products.Products[3], 2)
      };
      Orders.Add(Order.Create(
        new Guid("22222222-2222-2222-2222-222222222222"),
        Customers.Customers[1],
        productLines2,
        OrderStatus.Completed));

      var productLines3 = new List<ProductLine>
      {
        ProductLine.Create(Guid.NewGuid(), Products.Products[3], 2),
      };
      Orders.Add(Order.Create(
        new Guid("33333333-3333-3333-3333-333333333333"),
        Customers.Customers[3],
        productLines3,
        OrderStatus.Completed));
    }
  }
}
