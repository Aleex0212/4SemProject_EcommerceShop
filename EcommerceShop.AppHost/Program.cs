var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.EcommerceShop_Blazor>("ecommerceshop-blazor");

builder.Build().Run();
