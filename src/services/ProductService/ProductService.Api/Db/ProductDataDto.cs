using EcommerceShop.Common.Dto;

namespace ProductService.Api.Db
{
  public class ProductDataDto
  {
    public List<ProductDto> Products { get; set; } = new();

    public ProductDataDto()
    {
      Products.Add(new ProductDto
      {
        Id = new Guid("11111111-1111-1111-1111-111111111111"),
        Name = "Product1",
        Price = 100,
        Amount = 1000
      });

      Products.Add(new ProductDto
      {
        Id = new Guid("22222222-2222-2222-2222-222222222222"),
        Name = "Product2",
        Price = 200,
        Amount = 1000
      });

      Products.Add(new ProductDto
      {
        Id = new Guid("33333333-3333-3333-3333-333333333333"),
        Name = "Product3",
        Price = 300,
        Amount = 1000
      });

      Products.Add(new ProductDto
      {
        Id = new Guid("44444444-4444-4444-4444-444444444444"),
        Name = "Product4",
        Price = 400,
        Amount = 1000
      });

      Products.Add(new ProductDto
      {
        Id = new Guid("55555555-5555-5555-5555-555555555555"),
        Name = "Product5",
        Price = 500,
        Amount = 1000
      });

      Products.Add(new ProductDto
      {
        Id = new Guid("66666666-6666-6666-6666-666666666666"),
        Name = "Product6",
        Price = 600,
        Amount = 1000
      });

      Products.Add(new ProductDto
      {
        Id = new Guid("77777777-7777-7777-7777-777777777777"),
        Name = "Product7",
        Price = 700,
        Amount = 1000
      });

      Products.Add(new ProductDto
      {
        Id = new Guid("88888888-8888-8888-8888-888888888888"),
        Name = "Product8",
        Price = 800,
        Amount = 1000
      });
    }
  }
}
