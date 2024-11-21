using System.ComponentModel.DataAnnotations;

namespace Ecommerce.FrontEnd.ViewModels
{
  public class ProductViewModel
  {
    [Required(ErrorMessage = "Navn er påkrævet")]
    [StringLength(50)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "pris er påkrævet")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "antal er påkrævet")]
    public int Amount { get; set; }
  }
}
