using Aspire.Hosting.Dapr;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// PubSub Channels 
var daprStateStore = builder.AddDaprStateStore("daprStateStore");
var CustomerChannel = builder.AddDaprPubSub("CustomerChannel");
var OrderChannel = builder.AddDaprPubSub("OrderChannel");
var ProductChannel = builder.AddDaprPubSub("ProductChannel");
var CartChannel = builder.AddDaprPubSub("CartChannel");

// Project References
builder
    .AddProject<OrderService_Api>("OrderService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        DaprGrpcPort = 50005,
        DaprHttpPort = 3500
    })
    .WithReference(daprStateStore)
    .WithReference(CustomerChannel)
    .WithReference(OrderChannel)
    .WithReference(ProductChannel)
    .WithReference(CartChannel);

builder
    .AddProject<ProductService_Api>("ProductService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        DaprGrpcPort = 50006,
        DaprHttpPort = 3501
    })
    .WithReference(ProductChannel)
    .WithReference(OrderChannel);

builder
    .AddProject<CustomerService_Api>("CustomerService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        DaprGrpcPort = 50007,
        DaprHttpPort = 3502
    })
    .WithReference(CustomerChannel)
    .WithReference(ProductChannel);

builder
    .AddProject<CartService_Api>("CartService")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        DaprGrpcPort = 50008,
        DaprHttpPort = 3503
    })
    .WithReference(CartChannel)
    .WithReference(ProductChannel)
    .WithReference(OrderChannel);

string? daprPath = Environment.GetEnvironmentVariable("DAPR_PATH", EnvironmentVariableTarget.User);
builder.AddDapr(opts => opts.DaprPath = daprPath ?? "default/dapr/path");

var app = builder.Build();
app.Run();