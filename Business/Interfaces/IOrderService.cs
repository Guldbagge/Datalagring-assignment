using System.Threading.Tasks;
using Shared.Dtos;
using Infrastructure.Entities;

namespace Business.Interfaces;

public interface IOrderService
{
    Task<bool> PlaceOrderAsync(OrderDto orderDto);
    Task<bool> CreateOrderAsync(OrderEntity orderEntity);
    Task<bool> UpdateOrderAsync(OrderEntity updatedOrderEntity);
    Task<bool> DeleteOrderAsync(int orderId);
    Task<OrderEntity> GetOrderAsync(GetOneOrderDto getOneOrderDto);
}
