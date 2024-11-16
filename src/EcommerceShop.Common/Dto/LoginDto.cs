using System.ComponentModel.DataAnnotations;

namespace EcommerceShop.Common.Dto
{
  public class LoginDto
  {
    [Required(ErrorMessage = "Husk at skrive en email")]
    //[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Emailadressen er ikke korekt")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Husk at skrive en adgangskoden")]
    //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = " Adgangskoden skal være på minimum 8 tegn, mindst 1 stort bogstav, mindst 1 lille bogstav, 1 tal og 1 specialtegn.")]
    public string Password { get; set; } = string.Empty;
  }
}
