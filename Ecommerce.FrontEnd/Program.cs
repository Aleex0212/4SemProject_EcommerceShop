using Blazored.SessionStorage;
using Ecommerce.FrontEnd;
using Ecommerce.FrontEnd.Interfaces.Refit;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

#region ServiceRegistration
builder.Services.AddBlazoredSessionStorage();
#endregion

#region Refit Interface Registration

builder.Services.AddRefitClient<IAuthApi>()
  .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7136/"));

builder.Services
  .AddRefitClient<IProductApi>()
  .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7136/"));

#endregion

#region Radzen Components
builder.Services.AddScoped<NotificationService>();
#endregion

await builder.Build().RunAsync();
