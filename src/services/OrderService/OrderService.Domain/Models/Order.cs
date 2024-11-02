namespace OrderService.Domain.Models
{
  public class Order
  {
    public Guid? Id { get; set; } 

    public Customer Customer { get; private set; }

    public List<ProductLine> ProductLines { get; private set; }

    public OrderStatus Status { get; set; }

    public decimal TotalPrice { get; set; }

    private Order(Guid? id, Customer customer, List<ProductLine> productLines, 
      OrderStatus orderStatus)
    {
      Id = id;
      Customer = customer;  
      ProductLines = productLines;
      Status = orderStatus;
      TotalPrice = ProductLines.Sum(p => p.Quantity * p.Product.Price);
    }

    public static Order Create(Guid id, Customer customer, 
      List<ProductLine>? productLines, OrderStatus orderStatus)
    {
      return new Order(id, customer, productLines!, orderStatus);
    }
  }
}
