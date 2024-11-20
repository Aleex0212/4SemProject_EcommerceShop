using OrderService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Interfaces
{
  public interface IQueryRepository
  {
    IEnumerable<Order> GetAllOrders();
    Order GetOrder(Guid id);
  }
}
