using System.ComponentModel.DataAnnotations;

namespace Ecommerce.FrontEnd.ViewModels
{
  public class ProductViewModel
  {
    [Required(ErrorMessage = "Id er påkrævet")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Navn er påkrævet")]
    [StringLength(50, ErrorMessage = "Navn må ikke være længere end 50 tegn.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Pris er påkrævet")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Prisen skal være større end nul.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Mængde er påkrævet")]
    [Range(1, int.MaxValue, ErrorMessage = "Antal skal være mindst 1.")]
    public int Amount { get; set; }
  }
}
