using System.ComponentModel.DataAnnotations;

namespace EcommerceShop.Common.Dto
{
  public class LoginDto
  {
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email Address is not in correct format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", 
      ErrorMessage = " Password must be greater than 8 characters, at least 1 capital letter, " +
                     "at least 1 lower letter, 1 number and 1 specials character")]
    public string Password { get; set; } = string.Empty;
  }
}
