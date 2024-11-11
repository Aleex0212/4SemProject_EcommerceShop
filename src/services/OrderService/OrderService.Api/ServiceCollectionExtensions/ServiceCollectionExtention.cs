using Microsoft.EntityFrameworkCore;
using OrderService.Api.Mappers;
using OrderService.Application.Interfaces;
using OrderService.Application.Services;
using OrderService.Persistence.Context;
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
      services.AddTransient<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IOrderRepository, OrderRepository>();


      var connectionString = configuration.GetConnectionString("OrderDatabase") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
      services.AddDbContext<OrderContext>(options =>
      {
        options.UseSqlServer(connectionString);
      });
    }
  }
}
