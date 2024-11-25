using System.ComponentModel.DataAnnotations;
using EcommerceShop.Common.Enum;

namespace Ecommerce.FrontEnd.ViewModels
{
  public class CustomerViewModel
  {
    [Required(ErrorMessage = "Id er påkrævet")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Navn er påkrævet")]
    [StringLength(50, ErrorMessage = "Navn må ikke være længere end 50 tegn.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Brugertype er påkrævet")]
    public UserTypes UserType { get; set; }

    [Required(ErrorMessage = "Telefonnummer er påkrævet")]
    [Phone(ErrorMessage = "Telefonnummeret er ikke korrekt.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Adresse er påkrævet")]
    [StringLength(100, ErrorMessage = "Adressen må ikke være længere end 100 tegn.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Email er påkrævet")]
    [EmailAddress(ErrorMessage = "Email-adressen er ikke i korrekt format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Adgangskode er påkrævet")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
      ErrorMessage = "Adgangskoden skal være længere end 8 tegn, indeholde mindst 1 stort bogstav, " +
                     "mindst 1 lille bogstav, 1 tal og 1 specialtegn.")]
    public string Password { get; set; } = string.Empty;
  }
}
