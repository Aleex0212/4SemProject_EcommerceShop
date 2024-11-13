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
        }
    }
}
