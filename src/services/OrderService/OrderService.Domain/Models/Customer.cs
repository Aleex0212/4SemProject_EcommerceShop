namespace OrderService.Domain.Models
{
  public class Customer
  {
    public Guid? Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }

    private Customer(Guid id, string name, string email, string phone, string address)
    {
      Name = name;
      Email = email;
      Phone = phone;
      Address = address;
    }

    public static Customer Crate(Guid id, string name, string email, string phone, string address)
    {
      return new Customer(id = Guid.NewGuid(), name, email, phone, address);
    }
  }
}
