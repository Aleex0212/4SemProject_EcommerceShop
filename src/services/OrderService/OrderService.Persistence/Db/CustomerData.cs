using OrderService.Domain.Models;

namespace OrderService.Persistence.Db
{
  public class CustomerData
  {
    public List<Customer> Customers { get; }

    public CustomerData()
    {
      Customers = new List<Customer>();
      Customers.Add(Customer.Crate(
        Guid.NewGuid(),
        "Customer1",
        "Customer1Email@Mail.dk",
        "11111111",
        "Customer1Address"));

      Customers.Add(Customer.Crate(
      Guid.NewGuid(),
      "Customer2",
      "Customer2Email@Mail.dk",
      "22222222",
      "Customer2Address"));

      Customers.Add(Customer.Crate(
      Guid.NewGuid(),
      "Customer3",
      "Customer3Email@Mail.dk",
      "33333333",
      "Customer3Address"));

      Customers.Add(Customer.Crate(
      Guid.NewGuid(),
      "Customer4",
      "Customer4Email@Mail.dk",
      "44444444",
      "Customer4Address"));
    }
  }
}
