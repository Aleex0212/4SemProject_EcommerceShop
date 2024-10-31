using EcommerceShop.Common.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Application.Services.Command;
using ProductService.Application.Services.Query;
using ProductService.Persistence;
using ProductService.Persistence.Context;

namespace ProductService.Api.ServiceCollectionExtensions
{
  internal static class ServiceCollectionExtensions
  {
    internal static void ServiceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDaprClient();
      services.AddSignalR();

      // EF Core DB Context
      services.AddDbContext<ProductDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("ProductDefaultConnection")));

      // Application Services
      services.AddScoped<IQueryService, QueryService>();
      services.AddScoped<ICommandService, CommandService>();

      // Persistence Services
      services.AddScoped<IRepository, Repository>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      // EF Core DB Context
      services.AddDbContext<ProductDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("ProductDefaultConnection")));
    }
  }
}