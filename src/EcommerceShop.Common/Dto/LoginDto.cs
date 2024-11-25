using System.ComponentModel.DataAnnotations;

namespace EcommerceShop.Common.Dto
{
  public class LoginDto
  {
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email Address is not in correct format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string HashedPassword { get; set; } = string.Empty;
  }
}
