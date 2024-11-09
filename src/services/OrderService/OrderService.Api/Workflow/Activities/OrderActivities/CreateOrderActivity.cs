using Dapr.Workflow;

namespace OrderService.Api.Workflow.Activities.OrderActivities
{
  public class CreateOrderActivity : IWorkflowActivity
  {
    public Task<object?> RunAsync(WorkflowActivityContext context, object? input)
    {
      throw new NotImplementedException();
    }
    public Type InputType { get; }
    public Type OutputType { get; }
  }
}
