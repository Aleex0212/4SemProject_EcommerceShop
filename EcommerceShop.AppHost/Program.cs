using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Projects with Dapr sidecar
builder.AddProject<EcommerceShop_Blazor>("ecommerceshop-blazor")
    .WithDaprSidecar("ecommerce-shop");

builder.AddProject<SagaOrchestrator>("sagaorchestrator")
    .WithDaprSidecar("saga-orchestrator");

var.AddProject<Projects.EcommerceShop_FrontEnd>("ecommerceshop-frontend");

var app = builder.Build();

app.UseRouting();
app.UseCloudEvents(); 
app.MapControllers(); 

app.Run();

