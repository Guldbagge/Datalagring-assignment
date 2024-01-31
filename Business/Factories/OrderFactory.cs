using Infrastructure.Entities;
using Shared.Dtos;
using Shared.Utils;
using System;
using System.Linq;

namespace Business.Factories;

public static class OrderFactory
{
    public static OrderEntity CreateOrderEntity(string articleNumber, int userId, int quantity)
    {
        try
        {
            var orderEntity = new OrderEntity
            {
                UserId = userId,
            };

            var orderRowEntity = new OrderRowEntity
            {
                ArticleNumber = articleNumber,
                Quantity = quantity
            };

            orderEntity.OrderRows.Add(orderRowEntity);

            Logger.Log("OrderEntity created successfully.", "OrderFactory.CreateOrderEntity", LogTypes.Info);
            return orderEntity;
        }
        catch (Exception ex)
        {
            Logger.Log($"Error creating OrderEntity: {ex.Message}", "OrderFactory.CreateOrderEntity", LogTypes.Error);
            return null!;
        }
    }

    public static OrderDto CreateOrderDto(string articleNumber, int userId, int quantity)
    {
        try
        {
            var orderDto = new OrderDto
            {
                ArticleNumber = articleNumber,
                UserId = userId,
                Quantity = quantity
            };

            Logger.Log("OrderDto created successfully.", "OrderFactory.CreateOrderDto", LogTypes.Info);
            return orderDto;
        }
        catch (Exception ex)
        {
            Logger.Log($"Error creating OrderDto: {ex.Message}", "OrderFactory.CreateOrderDto", LogTypes.Error);
            return null!;
        }
    }

    public static OrderEntity UpdateOrderEntity(OrderEntity existingOrder, string newArticleNumber, int newQuantity)
    {
        try
        {
            if (existingOrder != null)
            {
                var orderRowEntity = existingOrder.OrderRows.FirstOrDefault();
                if (orderRowEntity != null)
                {
                    orderRowEntity.ArticleNumber = newArticleNumber;
                    orderRowEntity.Quantity = newQuantity;

                    Logger.Log("OrderEntity updated successfully.", "OrderFactory.UpdateOrderEntity", LogTypes.Info);
                    return existingOrder;
                }
            }

            Logger.Log("Unable to update OrderEntity: Order or OrderRow not found.", "OrderFactory.UpdateOrderEntity", LogTypes.Error);
            return null!;
        }
        catch (Exception ex)
        {
            Logger.Log($"Error updating OrderEntity: {ex.Message}", "OrderFactory.UpdateOrderEntity", LogTypes.Error);
            return null!;
        }
    }

    public static bool DeleteOrderEntity(OrderEntity orderEntity)
    {
        try
        {
            if (orderEntity != null)
            {
                Logger.Log("OrderEntity deleted successfully.", "OrderFactory.DeleteOrderEntity", LogTypes.Info);
                return true;
            }

            Logger.Log("Unable to delete OrderEntity: Order not found.", "OrderFactory.DeleteOrderEntity", LogTypes.Error);
            return false;
        }
        catch (Exception ex)
        {
            Logger.Log($"Error deleting OrderEntity: {ex.Message}", "OrderFactory.DeleteOrderEntity", LogTypes.Error);
            return false;
        }
    }
}
