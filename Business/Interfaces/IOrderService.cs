using Infrastructure.Entities;
using Shared.Dtos;

namespace Business.Interfaces;

public interface IOrderService
{
    Task<bool> PlaceOrderAsync(OrderDto orderDto);
    Task<bool> CreateOrderAsync(OrderEntity orderEntity);
}
