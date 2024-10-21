using EcommerceShop.Common.UnitOfWork;
using EcommerceShop.Common.UnitOfWork.Interfaces;
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

            //Application Services
            services.AddScoped<IProductService, Application.Services.ProductService>();

            //Persistence Services
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork<ProductDbContext>, UnitOfWork<ProductDbContext>>();

            //EF Core DB Context
            services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ProductDefaultConnection")));
        }
    }
}