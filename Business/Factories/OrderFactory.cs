using Infrastructure.Entities;
using Shared.Dtos;
using Shared.Utils;
using System;

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
}
