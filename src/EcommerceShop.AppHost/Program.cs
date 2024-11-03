using Aspire.Hosting.Dapr;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// PubSub Channels 
var daprStateStore = builder.AddDaprStateStore("daprStateStore");
var pubSub = builder.AddDaprPubSub("PubSub");


//Project References
builder.AddProject<Gateway_Api>("OrderGateway")
    .WithDaprSidecar()
    .WithReference(daprStateStore)
    .WithReference(pubSub);


builder
    .AddProject<OrderService_Api>("OrderService")
    .WithDaprSidecar()
    .WithReference(daprStateStore)
    .WithReference(pubSub);

builder
    .AddProject<ProductService_Api>("ProductService")
    .WithDaprSidecar()
    .WithReference(pubSub);

builder
    .AddProject<CustomerService_Api>("CustomerService")
    .WithDaprSidecar()
    .WithReference(pubSub);

string? daprPath = Environment.GetEnvironmentVariable("DAPR_PATH", EnvironmentVariableTarget.User);
builder.AddDapr(opts => opts.DaprPath = daprPath ?? "default/dapr/path");

var app = builder.Build();
app.Run();