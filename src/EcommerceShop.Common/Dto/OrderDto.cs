using EcommerceShop.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace EcommerceShop.Common.Dto
{
  public class OrderDto
  {
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Customer is required")]
    public UserDto Customer { get; set; }

    [Required(ErrorMessage = "Product line is required")]
    public required List<ProductLineDto> ProductLines { get; set; }

    [Required(ErrorMessage = "status is required")]
    public OrderStatus Status { get; set; }

    [Required(ErrorMessage = "Total price is required")]
    public decimal TotalPrice { get; set; }
  }
}
