namespace OrderService.Domain.Models
{
  public class Product
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    private Product(Guid id, string name, decimal price)
    {
      Id = id;
      Name = name;
      Price = price;
    }

    public static Product Create(Guid id, string name, decimal price)
    {
      return new Product(id, name, price);
    }
  }
}
