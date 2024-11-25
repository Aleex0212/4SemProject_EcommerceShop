using EcommerceShop.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.FrontEnd.ViewModels
{
  public class OrderViewModel
  {
    [Required(ErrorMessage = "Id er påkrævet")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Kunde er påkrævet")]
    public UserViewModel Customer { get; set; }

    [Required(ErrorMessage = "Produktlinje er påkrævet")]
    public required List<ProductLineViewModel> ProductLines { get; set; }

    [Required(ErrorMessage = "Status er påkrævet")]
    public OrderStatus Status { get; set; }

    [Required(ErrorMessage = "Totalpris er påkrævet")]
    public decimal TotalPrice { get; set; }
  }
}
