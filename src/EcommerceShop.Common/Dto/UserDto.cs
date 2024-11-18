using EcommerceShop.Common.Enum;

namespace EcommerceShop.Common.Dto
{
  public class UserDto
  {
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserTypes UserType { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
  }
}
