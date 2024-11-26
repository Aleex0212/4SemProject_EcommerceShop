using Blazored.SessionStorage;
using Ecommerce.FrontEnd;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Refit;
using System.Text.Json.Serialization;
using Ecommerce.FrontEnd.AutoMapper;
using Ecommerce.FrontEnd.Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

#region Refit Interface Registration
var serializer = SystemTextJsonContentSerializer.GetDefaultJsonSerializerOptions();

serializer.Converters.Remove(serializer.Converters.Single(x => x.GetType().Equals(typeof(JsonStringEnumConverter))));
var refitSettings = new RefitSettings
{
  ContentSerializer = new SystemTextJsonContentSerializer(serializer)
};

builder.Services.AddRefitClient<IAuthGatewayApi>(refitSettings)
  .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7136/"));

builder.Services
  .AddRefitClient<IProductGatewayApi>(refitSettings)
  .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7136/"));

builder.Services.AddRefitClient<IOrderGatewayApi>(refitSettings)
  .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7136/"));

builder.Services.AddRefitClient<IUserGatewayApi>(refitSettings)
  .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7136/"));
#endregion

#region ServiceRegistration
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<NotificationService>();
#endregion

#region AutoMapperProfile
builder.Services.AddAutoMapper(config =>
{
  config.AddProfile<MappingProfile>(); 
}, typeof(MappingProfile).Assembly);
#endregion

await builder.Build().RunAsync();
