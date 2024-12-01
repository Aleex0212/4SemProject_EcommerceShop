using Dapr.Workflow;

namespace OrderService.Api.Workflow.Activities
{
  public class NotificationActivity : WorkflowActivity<IEnumerable<string>, bool>
  {
    public override async Task<bool> RunAsync(WorkflowActivityContext context, IEnumerable<string> inputs)
    {
      try
      {
        foreach (var result in inputs)
        {
          Console.WriteLine(result);
        }
        await Task.Delay(1);
        return true;
      }
      catch(Exception ex)
      {
        return false;
      }
    }
  }
}
