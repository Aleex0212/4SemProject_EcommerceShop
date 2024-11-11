var builder = DistributedApplication.CreateBuilder(args);

// PubSub Channels 
var daprStateStore = builder.AddDaprStateStore("daprStateStore");
var pubSub = builder.AddDaprPubSub("pubsub");


builder.AddProject<Projects.Gateway_Api>("gateway-api")
  .WithDaprSidecar(
  //new DaprSidecarOptions
  //{
  //  DaprGrpcPort = 50005,
  //  DaprHttpPort = 3500
  //}
  )
      .WithReference(daprStateStore)
    .WithReference(pubSub);

builder.AddProject<Projects.CustomerService_Api>("customerservice-api")
     .WithDaprSidecar(
  //new DaprSidecarOptions
  //{
  //  DaprGrpcPort = 50006,
  //  DaprHttpPort = 3501
  //}
  )
         .WithReference(daprStateStore)
    .WithReference(pubSub);

builder.AddProject<Projects.OrderService_Api>("orderservice-api")
      .WithDaprSidecar(
  //new DaprSidecarOptions
  //{
  //  DaprGrpcPort = 50007,
  //  DaprHttpPort = 3502
  //}
  )
          .WithReference(daprStateStore)
    .WithReference(pubSub);

builder.AddProject<Projects.ProductService_Api>("productservice-api")
      .WithDaprSidecar(
  //new DaprSidecarOptions
  //{
  //  DaprGrpcPort = 50008,
  //  DaprHttpPort = 3503
  //}
  )
          .WithReference(daprStateStore)
    .WithReference(pubSub);

builder.AddProject<Projects.PaymentService_Api>("paymentservice-api")
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







builder.Build().Run();
