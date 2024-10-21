using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interfaces;
using ProductService.Persistence;
using ProductService.Persistence.ProductDbContext;

namespace ProductService.Api.ServiceCollectionExtensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void ServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDaprClient();
            services.AddSignalR();
            services.AddScoped<IProductService, Application.Services.ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ProductDefaultConnection")));
        }
    }
}