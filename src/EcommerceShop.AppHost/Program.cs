using Aspire.Hosting.Dapr;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// PubSub Channels 
var daprStateStore = builder.AddDaprStateStore("daprStateStore");
var customerChannel = builder.AddDaprPubSub("CustomerChannel");
var orderChannel = builder.AddDaprPubSub("OrderChannel");
var productChannel = builder.AddDaprPubSub("ProductChannel");
var cartChannel = builder.AddDaprPubSub("CartChannel");

//Project References
builder.AddProject<Gateway_Api>("OrderGateway")
    .WithDaprSidecar(new DaprSidecarOptions
    {
      DaprGrpcPort = 50004,
      DaprHttpPort = 3501
    })
    .WithReference(daprStateStore)
    .WithReference(customerChannel)
    .WithReference(orderChannel)
    .WithReference(productChannel)
    .WithReference(cartChannel);

builder
    .AddProject<OrderService_Api>("OrderService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
      DaprGrpcPort = 50005,
      DaprHttpPort = 3502
    })
    .WithReference(daprStateStore)
    .WithReference(customerChannel)
    .WithReference(orderChannel)
    .WithReference(productChannel)
    .WithReference(cartChannel);

builder
    .AddProject<ProductService_Api>("ProductService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
      DaprGrpcPort = 50006,
      DaprHttpPort = 3503
    })
    .WithReference(productChannel)
    .WithReference(orderChannel);

builder
    .AddProject<CustomerService_Api>("CustomerService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
      DaprGrpcPort = 50007,
      DaprHttpPort = 3504
    })
    .WithReference(customerChannel)
    .WithReference(productChannel);

string? daprPath = Environment.GetEnvironmentVariable("DAPR_PATH", EnvironmentVariableTarget.User);
builder.AddDapr(opts => opts.DaprPath = daprPath ?? "default/dapr/path");

var app = builder.Build();
app.Run();