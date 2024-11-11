using Dapr.Workflow;
using OrderService.Api.ServiceCollectionExtensions;
using OrderService.Api.Workflow;
using OrderService.Api.Workflow.Activities.CompensationActivity;
using OrderService.Api.Workflow.Activities.ExternalActivities;
using OrderService.Api.Workflow.Activities.InternalActivities;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

#region Dapr setup
var daprGrpcPort = Environment.GetEnvironmentVariable("DAPR_GRPC_PORT");
if (string.IsNullOrEmpty(daprGrpcPort))
{
  daprGrpcPort = "50001"; //altid sæt den til 50001
  Environment.SetEnvironmentVariable("DAPR_GRPC_PORT", daprGrpcPort);
}

var daprHttpPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
if (string.IsNullOrEmpty(daprHttpPort))
{
  daprHttpPort = "5005"; //altid sæt den til 50001
  Environment.SetEnvironmentVariable("DAPR_HTTP_PORT", daprHttpPort);
}

builder.Services.AddControllers();

builder.Services.AddDaprClient(config => config.UseGrpcEndpoint($"http://localhost:{daprGrpcPort}").UseHttpEndpoint($"http://localhost:{daprHttpPort}"));

builder.Services.AddDaprWorkflow(options =>
{
  //Workflows
  options.RegisterWorkflow<CreateOrderWorkflow>();

  //Compensating Activities 
  options.RegisterActivity<CancelOrderActivity>();
  options.RegisterActivity<ReleaseProductActivity>();

  //External Activities 
  options.RegisterActivity<AuthorizePaymentActivity>();
  options.RegisterActivity<ReserveProductActivity>();
  options.RegisterActivity<ValidateCustomerActivity>();

  //Internal Activities 
  options.RegisterActivity<ConfirmOrderActivity>();

});
#endregion

//Service Registration
builder.Services.ServiceRegistration(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseAuthorization();

#region dapr setup
app.UseCloudEvents(); //sørger for at medsendte parametre som DTO'er kan deserialiseres.
app.MapSubscribeHandler(); // kun ved explicit pubsub.

#endregion

app.MapControllers();

app.Run();
