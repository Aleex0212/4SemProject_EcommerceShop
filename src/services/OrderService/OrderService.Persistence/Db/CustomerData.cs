using OrderService.Domain.Models;

namespace OrderService.Persistence.Db
{
  public class CustomerData
  {
    public List<Customer> Customers { get; } = new();

    public CustomerData()
    {
      Customers.Add(Customer.Crate(
      new Guid("11111111-1111-1111-1111-111111111111"),
      "Customer1",
      "Customer@Mail.dk",
      "22222222",
      "Customer2Address"));

      Customers.Add(Customer.Crate(
      new Guid("22222222-2222-2222-2222-222222222222"),
      "Admin",
      "Admin@Mail.dk",
      "33333333",
      "Customer3Address"));

      Customers.Add(Customer.Crate(
      new Guid("33333333-3333-3333-3333-333333333333"),
      "User",
      "User@Mail.dk",
      "44444444",
      "Customer4Address"));
    }
  }
}
