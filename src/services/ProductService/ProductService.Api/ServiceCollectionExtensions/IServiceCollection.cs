using ProductService.Application.Interfaces;

namespace ProductService.Api.ServiceCollectionExtensions
{
    internal static class IServiceCollection
    {
        internal static void ServiceRegistration(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services.AddDaprClient();
            services.AddSignalR();

            services.AddScoped<IProductService, Application.Services.ProductService>();
        }
    }
}