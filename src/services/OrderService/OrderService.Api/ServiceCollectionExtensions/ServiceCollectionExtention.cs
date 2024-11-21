using OrderService.Api.Mappers;
using OrderService.Application.Interfaces;
using OrderService.Application.Services;
using OrderService.Persistence.Db;
using OrderService.Persistence.Repositories;

namespace OrderService.Api.ServiceCollectionExtensions
{
  internal static class ServiceCollectionExtensions
  {
    internal static void ServiceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
      // Application Services
      services.AddScoped<ICommandService, CommandService>();
      services.AddScoped<IQueryService, QueryService>();

      // Api Services
      services.AddScoped<DomainMapper>();

      //Repository services
      services.AddScoped<ICommandRepository, CommandRepository>();
      services.AddScoped<IQueryRepository, QueryRepository>();

      //Database 
      services.AddSingleton<OrderData>();
      services.AddSingleton<CustomerData>();
      services.AddSingleton<ProductData>();
    }
  }
}
