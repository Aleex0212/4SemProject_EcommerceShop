using Aspire.Hosting.Dapr;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// PubSub Channels 
var daprStateStore = builder.AddDaprStateStore("daprStateStore");
var pubSub = builder.AddDaprPubSub("PubSub");


//Project References
builder.AddProject<Gateway_Api>("OrderGateway")
    .WithDaprSidecar(new DaprSidecarOptions
    {
      DaprGrpcPort = 50004,
      DaprHttpPort = 3501
    })
    .WithReference(daprStateStore)
    .WithReference(pubSub);


builder
    .AddProject<OrderService_Api>("OrderService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
      DaprGrpcPort = 50005,
      DaprHttpPort = 3502
    })
    .WithReference(daprStateStore)
    .WithReference(pubSub);

builder
    .AddProject<ProductService_Api>("ProductService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
      DaprGrpcPort = 50006,
      DaprHttpPort = 3503
    })
    .WithReference(pubSub);

builder
    .AddProject<CustomerService_Api>("CustomerService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
      DaprGrpcPort = 50007,
      DaprHttpPort = 3504
    })
    .WithReference(pubSub);

string? daprPath = Environment.GetEnvironmentVariable("DAPR_PATH", EnvironmentVariableTarget.User);
builder.AddDapr(opts => opts.DaprPath = daprPath ?? "default/dapr/path");

var app = builder.Build();
app.Run();