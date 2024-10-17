var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.EcommerceShop_Blazor>("ecommerceshop-blazor");

builder.AddProject<Projects.SagaOrchestrator>("sagaorchestrator");

builder.Build().Run();
