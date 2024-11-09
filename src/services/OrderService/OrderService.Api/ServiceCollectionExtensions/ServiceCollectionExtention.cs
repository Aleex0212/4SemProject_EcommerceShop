using OrderService.Application.Interfaces;
using OrderService.Application.Services;

namespace OrderService.Api.ServiceCollectionExtensions
{
  internal static class ServiceCollectionExtensions
  {
    internal static void ServiceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
      // Application Services
      services.AddScoped<ICommandService, CommandService>();
      services.AddScoped<IQueryService, QueryService>();
    }
  }
}
