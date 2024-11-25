using EcommerceShop.Umraco.Refit;
using EcommerceShop.Umraco.Controllers;
using Refit;
using EcommerceShop.Umraco;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

#region Refit Interface Registration

builder.Services.AddRefitClient<IAuthApi>()
  .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7136/"));
builder.Services.AddRefitClient<IProductApi>()
  .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7136/"));

#endregion

#region Services

builder.Services.AddDataProtection();

builder.Services.AddScoped<LoginController>();
builder.Services.AddSingleton<SessionStore>();
builder.Services.AddSingleton<LoggedInUser>();

#endregion

WebApplication app = builder.Build();

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
      u.UseBackOffice();
      u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
      u.UseBackOfficeEndpoints();
      u.UseWebsiteEndpoints();
    });

await app.RunAsync();
