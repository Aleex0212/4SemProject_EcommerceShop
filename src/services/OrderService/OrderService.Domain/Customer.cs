namespace OrderService.Domain
{
  public class Customer
  {
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    private Customer(Guid id, string name, string email, string phone, string address)
    {
      Id = id;
      Name = name;
      Email = email;
      Phone = phone;
      Address = address;
    }

    public static Customer Crate(Guid id, string name, string email, string phone, string address)
    {
      return new Customer(id,name,email, phone, address);
    }
  }
}
