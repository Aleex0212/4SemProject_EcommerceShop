namespace EcommerceShop.Common.Dto
{
  public class OrderDto
  {
    public Guid Id { get; set; }
    public CustomerDto CustomerDto { get; set; }
    public required List<ProductLineDto> ProductLinesDto { get; set; }
    public string? Status { get; init; } 
    public decimal TotalPrice { get; set; }
  }
}
