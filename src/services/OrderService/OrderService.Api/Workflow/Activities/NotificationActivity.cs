using Dapr.Workflow;

namespace OrderService.Api.Workflow.Activities
{
  public class NotificationActivity : WorkflowActivity<IEnumerable<string>, object?>
  {
    public override Task<object?> RunAsync(WorkflowActivityContext context, IEnumerable<string> inputs)
    {
      foreach (var result in inputs)
      {
        Console.WriteLine(result);
      }
      return null!;
    }
  }
}
