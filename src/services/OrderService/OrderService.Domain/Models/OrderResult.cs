namespace OrderService.Domain.Models
{
  public class OrderResult
  {
    public required Order Order { get; set; }
    public required OrderStatus Status { get; set; }
    public string? Message { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.Now;
  }
}
