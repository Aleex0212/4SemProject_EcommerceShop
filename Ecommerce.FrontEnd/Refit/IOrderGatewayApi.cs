﻿using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace Ecommerce.FrontEnd.Refit
{
  public interface IOrderGatewayApi
  {
    [Post(Routes.GatewayRoutes.OrderGatewayRoutes.Order)]
    Task CreateOrderAsync([Body] OrderDto order, [Header("Authorization")] string bearerToken);

    [Get(Routes.GatewayRoutes.OrderGatewayRoutes.GetByEmail)]
    Task<IEnumerable<OrderDto>> GetOrdersByCustomerEmail(string customerEmail, [Header("Authorization")] string bearerToken);
  }
}
