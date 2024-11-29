namespace OrderService.Domain.Models
{
  public class Product
  {
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    private Product(Guid id, string name, decimal price)
    {
      Id = id;
      Name = name;
      Price = price;
    }

    public static Product Create(Guid id, string name, decimal price)
    {
      return new Product(id = id, name, price);
    }
  }
}
