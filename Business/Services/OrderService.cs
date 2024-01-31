using Business.Factories;
using Business.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Shared.Dtos;
using Shared.Utils;
using System;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<bool> PlaceOrderAsync(OrderDto orderDto)
        {
            try
            {
                var orderEntity = OrderFactory.CreateOrderEntity(orderDto.ArticleNumber, orderDto.UserId, orderDto.Quantity);

                if (orderEntity != null)
                {
                    var result = await CreateOrderAsync(orderEntity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "OrderService.PlaceOrderAsync()", LogTypes.Error);
            }

            return false;
        }

        public async Task<bool> CreateOrderAsync(OrderEntity orderEntity)
        {
            try
            {
                Logger.Log($"_orderRepository is {_orderRepository}", "OrderService.CreateOrderAsync()", LogTypes.Info);

                orderEntity = await _orderRepository.CreateAsync(orderEntity, nameof(_orderRepository));

                if (orderEntity != null)
                {
                    Logger.Log("An order was placed successfully.", "OrderService.CreateOrderAsync()", LogTypes.Info);
                    return true;
                }
                else
                {
                    Logger.Log("OrderEntity is null after creation.", "OrderService.CreateOrderAsync()", LogTypes.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "OrderService.CreateOrderAsync()", LogTypes.Error);
            }

            return false;
        }

        public async Task<bool> UpdateOrderAsync(OrderEntity updatedOrderEntity)
        {
            try
            {
                var existingOrder = await _orderRepository.GetAsync(x => x.OrderId == updatedOrderEntity.OrderId);

                if (existingOrder != null)
                {
                    existingOrder.UserId = updatedOrderEntity.UserId;
                    existingOrder.OrderDate = updatedOrderEntity.OrderDate;

                    var result = await _orderRepository.UpdateAsync(x => x.OrderId == existingOrder.OrderId, existingOrder);
                    return result != null;
                }

                Logger.Log($"Order with ID {updatedOrderEntity.OrderId} not found.", "OrderService.UpdateOrderAsync()", LogTypes.Info);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "OrderService.UpdateOrderAsync()", LogTypes.Error);
                return false;
            }
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            try
            {
                var result = await _orderRepository.DeleteAsync(x => x.OrderId == orderId);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "OrderService.DeleteOrderAsync()", LogTypes.Error);
                return false;
            }
        }

        public async Task<OrderEntity> GetOrderAsync(GetOneOrderDto getOneOrderDto)
        {
            try
            {
                var orderEntity = await _orderRepository.GetAsync(x => x.OrderId == getOneOrderDto.OrderId);
                return orderEntity;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "OrderService.GetOrderAsync(GetOneOrderDto)", LogTypes.Error);
                return null!;
            }
        }
    }
}
