using System.ComponentModel.DataAnnotations;

namespace EcommerceShop.Common.Dto
{
  public class PaymentDto
  {
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(59)]
    public string Name { get; set; }

    [Required]
    [StringLength(200)]
    public string Description { get; set; }
  }
}
