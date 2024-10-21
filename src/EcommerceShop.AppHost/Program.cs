var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SagaOrchestrator>("sagaorchestrator");

//builder.AddProject<Projects.EcommerceShop_FrontEnd>("ecommerceshop-frontend");

builder.Build().Run();
