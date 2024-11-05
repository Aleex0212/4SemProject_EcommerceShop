using Aspire.Hosting.Dapr;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// PubSub Channels 
var daprStateStore = builder.AddDaprStateStore("daprStateStore");
var pubSub = builder.AddDaprPubSub("pubsub");


//Project References
builder.AddProject<Gateway_Api>("ordergateway")
    .WithDaprSidecar(
  //new DaprSidecarOptions
  //{
  //  DaprGrpcPort = 50005,
  //  DaprHttpPort = 3500
  //}
  )
    .WithReference(daprStateStore)
    .WithReference(pubSub);


builder
    .AddProject<OrderService_Api>("orderservice")
   .WithDaprSidecar(
  //new DaprSidecarOptions
  //{
  //  DaprGrpcPort = 50006,
  //  DaprHttpPort = 3501
  //}
  )
    .WithReference(daprStateStore)
    .WithReference(pubSub);

builder
    .AddProject<ProductService_Api>("productservice")
    .WithDaprSidecar(
  //new DaprSidecarOptions
  //{
  //  DaprGrpcPort = 50007,
  //  DaprHttpPort = 3502
  //}
  )
    .WithReference(daprStateStore)
    .WithReference(pubSub);

builder
    .AddProject<CustomerService_Api>("customerservice")
    .WithDaprSidecar(
  //new DaprSidecarOptions
  //{
  //  DaprGrpcPort = 50008,
  //  DaprHttpPort = 3503
  //}
  )
    .WithReference(daprStateStore)
    .WithReference(pubSub);

string? daprPath = Environment.GetEnvironmentVariable("DAPR_PATH", EnvironmentVariableTarget.User);
builder.AddDapr(opts => opts.DaprPath = daprPath ?? "default/dapr/path");

var app = builder.Build();
app.Run();