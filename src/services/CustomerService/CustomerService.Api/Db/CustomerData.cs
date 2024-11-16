
using EcommerceShop.Common.Dto;

namespace CustomerService.Db
{
  public class CustomerData
  {
    public List<CustomerDto> Customers { get; }

    public CustomerData()
    {
      Customers = new List<CustomerDto>();

      var customer1 = new CustomerDto()
      {
        Id = Guid.NewGuid(),
        Name = "Customer1",
        Email = "Customer1Email@Mail.dk",
        Password = "Customer1Password",
        Phone = "11111111",
        Address = "Customer1Address"
      };
      var customer2 = new CustomerDto()
      {
        Id = Guid.NewGuid(),
        Name = "Customer2",
        Email = "Customer2Email@Mail.dk",
        Password = "Customer2Password",
        Phone = "22222222",
        Address = "Customer2Address"
      };
      var customer3 = new CustomerDto()
      {
        Id = Guid.NewGuid(),
        Name = "Customer3",
        Email = "Customer3Email@Mail.dk",
        Password = "Customer3Password",
        Phone = "33333333",
        Address = "Customer3Address"
      };
      var customer4 = new CustomerDto()
      {
        Id = Guid.NewGuid(),
        Name = "Customer4",
        Email = "Customer4Email@Mail.dk",
        Password = "Customer4Password",
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
