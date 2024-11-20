using System.ComponentModel.DataAnnotations;

namespace EcommerceShop.Common.Dto
{
  public class ProductLineDto
  {
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Product is required")]
    public ProductDto Product { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    public int Quantity { get; set; }
  }
}
