namespace EcommerceShop.Common.Dto
{
  public class OrderDto
  {
    public Guid Id { get; set; }
    public CustomerDto Customer { get; set; }
    public required List<ProductLineDto> ProductLines { get; set; }
    public string? Status { get; init; }
    public decimal TotalPrice { get; set; }
  }
}
