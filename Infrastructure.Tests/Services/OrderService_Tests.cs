
using Business.Factories;
using Business.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Shared.Dtos;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests.Services
{
    public class OrderService_Tests
    {
        private readonly OrderService _orderService;
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new Mock<IOrderRepository>();

        public OrderService_Tests()
        {
            _orderService = new OrderService(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task PlaceOrderAsync_ShouldReturnTrue_WhenOrderEntityIsCreated()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                ArticleNumber = "12345",
                UserId = 1,
                Quantity = 5
            };

            _orderRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<OrderEntity>(), It.IsAny<string>())).ReturnsAsync(new OrderEntity { OrderId = 1 });

            // Act
            var result = await _orderService.PlaceOrderAsync(orderDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateOrderAsync_ShouldReturnTrue_WhenOrderEntityIsUpdated()
        {
            // Arrange
            var updatedOrderEntity = new OrderEntity
            {
                OrderId = 1,
                UserId = 2,
                OrderDate = DateTime.Now
            };

            _orderRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrderEntity, bool>>>()))
                               .ReturnsAsync(new OrderEntity { OrderId = 1 });

            _orderRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Expression<Func<OrderEntity, bool>>>(), It.IsAny<OrderEntity>()))
                               .ReturnsAsync(new OrderEntity { OrderId = 1 });

            // Act
            var result = await _orderService.UpdateOrderAsync(updatedOrderEntity);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldReturnTrue_WhenOrderIsDeleted()
        {
            // Arrange
            var orderId = 1;

            _orderRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Expression<Func<OrderEntity, bool>>>())).ReturnsAsync(true);

            // Act
            var result = await _orderService.DeleteOrderAsync(orderId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetOrderAsync_ShouldReturnOrderEntity_WhenOrderIdExists()
        {
            // Arrange
            var getOneOrderDto = new GetOneOrderDto { OrderId = 1 };

            _orderRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrderEntity, bool>>>()))
                               .ReturnsAsync(new OrderEntity { OrderId = 1 });

            // Act
            var result = await _orderService.GetOrderAsync(getOneOrderDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.OrderId);
        }
    }
}
