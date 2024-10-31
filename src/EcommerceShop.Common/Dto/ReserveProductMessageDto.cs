using System.ComponentModel.DataAnnotations;

namespace EcommerceShop.Common.Dto
{
  public class ReserveProductMessageDto
  {
    [Required] public Guid CustomerId { get; set; }
    [Required] public Guid OrderId { get; set; }
    [Required] public Guid ProductId { get; set; }
    [Required] public int Quantity { get; set; }
  }
}
