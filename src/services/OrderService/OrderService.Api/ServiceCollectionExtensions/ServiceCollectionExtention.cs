using Dapr.Workflow;
using OrderService.Api.Workflow;
using OrderService.Api.Workflow.Activities.ExternalActivities;
using OrderService.Api.Workflow.Activities.OrderActivity;
using OrderService.Application.Interfaces;
using OrderService.Application.Services;

namespace OrderService.Api.ServiceCollectionExtensions
{
  internal static class ServiceCollectionExtensions
  {
    internal static void ServiceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
      //Dapr 
      services.AddDaprWorkflow(options =>
      {
        //Workflows
        options.RegisterWorkflow<CreateOrderWorkflow>();

        //Activities 
        options.RegisterActivity<CreateOrderActivity>();
        options.RegisterActivity<ReserveProductActivity>();
      });

      // Application Services
      services.AddScoped<ICommandService, CommandService>();
      services.AddScoped<IQueryService, QueryService>();
    }
  }
}
