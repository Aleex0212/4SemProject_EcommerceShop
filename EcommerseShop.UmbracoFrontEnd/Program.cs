using EcommerceShop.UmbracoFrontEnd.Refit;
using EcommerceShop.UmbracoFrontEnd.Controllers;
using Refit;
using EcommerseShop.UmbracoFrontEnd.Controllers;
using EcommerseShop.UmbracoFrontEnd.SessionStores;
using EcommerseShop.UmbracoFrontEnd.Refit;

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
builder.Services.AddRefitClient<IUserApi>()
  .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7136/"));
builder.Services.AddRefitClient<IOrderApi>()
  .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7136/"));

#endregion

#region Services

builder.Services.AddDataProtection();

builder.Services.AddScoped<LoginController>();
builder.Services.AddScoped<ProductController>();
builder.Services.AddScoped<ContactController>();
builder.Services.AddScoped<BasketController>();
builder.Services.AddSingleton<SessionStore>();
builder.Services.AddSingleton<LoggedInUser>();
builder.Services.AddSingleton<Basket>();

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
