using System.ComponentModel.DataAnnotations;

namespace Ecommerce.FrontEnd.ViewModels
{
  public class ProductLineViewModel
  {
    [Required(ErrorMessage = "Id er påkrævet")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Produkt er påkrævet")]
    public ProductViewModel Product { get; set; }

    [Required(ErrorMessage = "Antal er påkrævet")]
    public int Quantity { get; set; }
  }
}
