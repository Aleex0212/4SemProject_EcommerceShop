using EcommerceShop.Common.Dto;
using System.ComponentModel.DataAnnotations;

namespace EcommerceShop.Common.IntegrationEvents
{
  public record IntegrationEventOutgoing
  {
    public string CorrelationId { get; init; } = String.Empty;
  }

  public record ReserveProductDto : IntegrationEventOutgoing
  {
    [Required] public string CorrelationId { get; set; }
    [Required] public List<ProductLineDto> Products { get; set; }
  }

}
