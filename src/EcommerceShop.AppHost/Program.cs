using Projects;

var builder = DistributedApplication.CreateBuilder(args);

//  PubSub Channels 
var daprStateStore = builder.AddDaprStateStore("daprStateStore");
var CustomerChannel = builder.AddDaprPubSub("CustomerChannel");
var OrderChannel = builder.AddDaprPubSub("OrderChannel");
var ProductChannel = builder.AddDaprPubSub("ProductChannel");
var CartChannel = builder.AddDaprPubSub("CartChannel");

// Project References
builder
    .AddProject<OrderService_Api>("OrderService")
    .WithDaprSidecar("order-service")
    .WithReference(daprStateStore)
    .WithReference(CustomerChannel)
    .WithReference(OrderChannel)
    .WithReference(ProductChannel)
    .WithReference(CartChannel);

builder
    .AddProject<ProductService_Api>("ProductService")
    .WithDaprSidecar("product-service")
    .WithReference(ProductChannel)
    .WithReference(OrderChannel);

builder
    .AddProject<CustomerService_Api>("CustomerService")
    .WithDaprSidecar("customer-service")
    .WithReference(CustomerChannel)
    .WithReference(ProductChannel);

builder
    .AddProject<CartService_Api>("CartService")
    .WithDaprSidecar("cart-service");

string? daprPath = Environment.GetEnvironmentVariable("DAPR_PATH", EnvironmentVariableTarget.User);
builder.AddDapr(opts => opts.DaprPath = daprPath ?? "default/dapr/path");

var app = builder.Build();
app.Run();