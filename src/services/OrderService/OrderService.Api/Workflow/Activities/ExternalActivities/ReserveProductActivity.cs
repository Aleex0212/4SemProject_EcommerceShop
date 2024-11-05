using Dapr.Client;
using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.IntegrationEvents;
using EcommerceShop.Common.Queues;

namespace OrderService.Api.Workflow.Activities.ExternalActivities
{
  public class ReserveProductActivity : WorkflowActivity<ProductDto, object?>
  {
    private readonly DaprClient _daprClient;
    private readonly ILogger<ReserveProductActivity> _logger;

    public ReserveProductActivity(DaprClient daprClient, ILogger<ReserveProductActivity> logger)
    {
      _daprClient = daprClient;
      _logger = logger;
    }

    public override async Task<object?> RunAsync(WorkflowActivityContext context, ProductDto productDto)
    {
      _logger.LogInformation($"Reserving Product: {productDto.Id}");

      var reserveProductDto = new ReserveProductDto()
      {
        CorrelationId = context.InstanceId,
        Products = new List<ProductLineDto>(),
      };

      await _daprClient.PublishEventAsync(PubSub.Channel, PubSub.ProductTopic.Reserve);
      return reserveProductDto;
    }
  }
}
