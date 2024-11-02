namespace OrderService.Domain.Models
{
  public class ProductLine
  {
    public int Id { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }

    private ProductLine(int id, Product product, int quantity)
    {
      Id = id;
      Product = product;
      Quantity = quantity;
    }
    public static ProductLine Create(int id, Product product, int quantity)
    {
      return new ProductLine(id, product, quantity);
    }
  }

}
