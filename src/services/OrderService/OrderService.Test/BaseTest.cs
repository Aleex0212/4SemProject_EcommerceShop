using OrderService.Api.Workflow;
using OrderService.Application.Services;
using OrderService.Domain.Models;
using OrderService.Persistence.Db;
using System.Reflection;

namespace OrderService.Test
{
  public abstract class BaseTest
  {
    protected static readonly Assembly DomainAssembly = typeof(Customer).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(CommandService).Assembly;
    protected static readonly Assembly PersistanceAssembly = typeof(CustomerData).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(CreateOrderWorkflow).Assembly;
  }
}
