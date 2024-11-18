var builder = DistributedApplication.CreateBuilder(args);

// State-store Channels
var daprStateStore = builder.AddDaprStateStore("daprStateStore");
// Pub-Sub Channels 
var pubSub = builder.AddDaprPubSub("pubsub");

builder.AddProject<Projects.Gateway_Api>("gateway-api")
  .WithDaprSidecar()
    .WithReference(daprStateStore)
    .WithReference(pubSub);

builder.AddProject<Projects.UserService_Api>("userservice-api")
  .WithDaprSidecar()
    .WithReference(daprStateStore)
    .WithReference(pubSub);

builder.AddProject<Projects.OrderService_Api>("orderservice-api")
  .WithDaprSidecar()
    .WithReference(daprStateStore)
    .WithReference(pubSub);

builder.AddProject<Projects.ProductService_Api>("productservice-api")
  .WithDaprSidecar()
    .WithReference(daprStateStore)
    .WithReference(pubSub);

builder.AddProject<Projects.PaymentService_Api>("paymentservice-api")
  .WithDaprSidecar()
    .WithReference(daprStateStore)
    .WithReference(pubSub);

string? daprPath = Environment.GetEnvironmentVariable("DAPR_PATH", EnvironmentVariableTarget.User);
builder.AddDapr(opts => opts.DaprPath = daprPath ?? "default/dapr/path");

builder.Build().Run();
