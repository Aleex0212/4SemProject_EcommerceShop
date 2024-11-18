using EcommerceShop.Common.Dto;

namespace UserService.Db
{
  public class CustomerData
  {
    public List<UserDto> Customers { get; }

    public CustomerData()
    {
      Customers = new List<UserDto>();

      var customer1 = new UserDto()
      {
        Id = Guid.NewGuid(),
        Name = "Customer1",
        Email = "Customer1Email@Mail.dk",
        Password = "Customer1Password",
        UserType = EcommerceShop.Common.Enum.UserTypes.Customer,
        Phone = "11111111",
        Address = "Customer1Address"
      };
      var customer2 = new UserDto()
      {
        Id = Guid.NewGuid(),
        Name = "Customer2",
        Email = "Customer2Email@Mail.dk",
        Password = "Customer2Password",
        UserType = EcommerceShop.Common.Enum.UserTypes.Customer,
        Phone = "22222222",
        Address = "Customer2Address"
      };
      var customer3 = new UserDto()
      {
        Id = Guid.NewGuid(),
        Name = "Customer3",
        Email = "Customer3Email@Mail.dk",
        Password = "Customer3Password",
        UserType = EcommerceShop.Common.Enum.UserTypes.Admin,
        Phone = "33333333",
        Address = "Customer3Address"
      };
      var customer4 = new UserDto()
      {
        Id = Guid.NewGuid(),
        Name = "Customer4",
        Email = "Customer4Email@Mail.dk",
        Password = "Customer4Password",
        UserType = EcommerceShop.Common.Enum.UserTypes.User,
        Phone = "44444444",
        Address = "Customer4Address"
      };
      Customers.Add(customer1);
      Customers.Add(customer2);
      Customers.Add(customer3);
      Customers.Add(customer4);
    }
  }
}
