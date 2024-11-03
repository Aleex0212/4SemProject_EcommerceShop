using Aspire.Hosting.Dapr;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// PubSub Channels 
//var customerChannel = builder.AddDaprPubSub("OrderChannel");
//var daprStateStore = builder.AddDaprStateStore("daprStateStore");
var orderChannel = builder.AddDaprPubSub("OrderChannel");
//var productChannel = builder.AddDaprPubSub("ProductChannel");
//var cartChannel = builder.AddDaprPubSub("CartChannel");

//Project References
builder.AddProject<Gateway_Api>("OrderGateway")
    .WithDaprSidecar(new DaprSidecarOptions
    {
      DaprGrpcPort = 50004,
      DaprHttpPort = 3501
    })
    .WithReference(orderChannel);

builder
    .AddProject<OrderService_Api>("OrderService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
      DaprGrpcPort = 50005,
      DaprHttpPort = 3502
    })
    .WithReference(orderChannel);

builder
    .AddProject<ProductService_Api>("ProductService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
      DaprGrpcPort = 50006,
      DaprHttpPort = 3503
    })
    .WithReference(orderChannel);

builder
    .AddProject<CustomerService_Api>("CustomerService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
      DaprGrpcPort = 50007,
      DaprHttpPort = 3504
    });

string? daprPath = Environment.GetEnvironmentVariable("DAPR_PATH", EnvironmentVariableTarget.User);
builder.AddDapr(opts => opts.DaprPath = daprPath ?? "default/dapr/path");

var app = builder.Build();
app.Run();