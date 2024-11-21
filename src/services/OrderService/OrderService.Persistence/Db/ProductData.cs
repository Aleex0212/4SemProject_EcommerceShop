using OrderService.Domain.Models;

namespace OrderService.Persistence.Db
{
  public class ProductData
  {
    public List<Product> Products { get; }

    public ProductData()
    {
      Products = new List<Product>();
      Products.Add(Product.Create(
        new Guid("11111111-1111-1111-1111-111111111111"),
        "Product1",
        100));

      Products.Add(Product.Create(
        new Guid("22222222-2222-2222-2222-222222222222"),
        "Product2",
        200));

      Products.Add(Product.Create(
        new Guid("33333333-3333-3333-3333-333333333333"),
        "Product3",
        300));

      Products.Add(Product.Create(
        new Guid("44444444-4444-4444-4444-444444444444"),
        "Product4",
        400));
    }
  }
}
