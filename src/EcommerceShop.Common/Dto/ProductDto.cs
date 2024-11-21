using System.ComponentModel.DataAnnotations;

namespace EcommerceShop.Common.Dto
{
  public class ProductDto
  {
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    public int Amount { get; set; }
  }
}