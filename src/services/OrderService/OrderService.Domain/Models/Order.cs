using EcommerceShop.Common.Enum;

namespace OrderService.Domain.Models
{
  public class Order
  {
    public Guid Id { get; private set; }

    public Customer Customer { get; private set; }

    public IEnumerable<ProductLine> ProductLines { get; private set; }

    public OrderStatus Status { get; private set; }

    public decimal TotalPrice { get; private set; }

    private Order(Guid id, Customer customer, IEnumerable<ProductLine> productLines,
      OrderStatus orderStatus)
    {
      Id = id;
      Customer = customer;
      ProductLines = productLines;
      Status = orderStatus;
      TotalPrice = ProductLines.Sum(p => p.Quantity * p.Product.Price);
    }

    public static Order Create(Guid id, Customer customer,
      IEnumerable<ProductLine>? productLines, OrderStatus orderStatus)
    {
      return new Order(id, customer, productLines!, orderStatus);
    }
  }
}
