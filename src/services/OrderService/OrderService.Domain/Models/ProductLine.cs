namespace OrderService.Domain.Models
{
  public class ProductLine
  {
    public Guid Id { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }

    private ProductLine(Guid id, Product product, int quantity)
    {
      Id = id;
      Product = product;
      Quantity = quantity;
    }
    public static ProductLine Create(Guid id, Product product, int quantity)
    {
      return new ProductLine(id = id, product, quantity);
    }
  }
}
