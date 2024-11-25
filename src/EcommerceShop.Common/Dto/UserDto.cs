using EcommerceShop.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace EcommerceShop.Common.Dto
{
  public class UserDto
  {
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50)]
    public string Name { get; set; }

    [Required(ErrorMessage = "UserType is required")]
    public UserTypes UserType { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [StringLength(100)]
    public string Address { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email Address is not in correct format")]
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
  }
}
