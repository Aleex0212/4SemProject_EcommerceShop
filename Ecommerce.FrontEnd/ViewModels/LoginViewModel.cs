using System.ComponentModel.DataAnnotations;

namespace Ecommerce.FrontEnd.ViewModels
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email Address is not in correct format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Adgangskode er påkrævet")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
      ErrorMessage = "Adgangskoden skal være længere end 8 tegn, indeholde mindst 1 stort bogstav, " +
                     "mindst 1 lille bogstav, 1 tal og 1 specialtegn.")]
    public string Password { get; set; } = string.Empty;
  }
}
