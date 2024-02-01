using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests.Repositories
{
    public class OrderRowRepository_Tests
    {
        private readonly CustomerContext _context;

        public OrderRowRepository_Tests()
        {
            _context = new CustomerContext(new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_OrderRowEntity_To_Context_And_SaveChanges()
        {
            // Arrange
            var repo = new OrderRowRepository(_context);
            var entity = new OrderRowEntity
            {
                OrderId = 1,
                ArticleNumber = "Article1",
                Quantity = 2
            };

            // Act
            var result = await repo.CreateAsync(entity);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, _context.OrderRow);
        }

        [Fact]
        public async Task GetAsync_Should_Retrieve_OrderRowEntity_From_Context()
        {
            // Arrange
            var repo = new OrderRowRepository(_context);
            var entity = new OrderRowEntity
            {
                OrderId = 2,
                ArticleNumber = "Article2",
                Quantity = 3
            };

            // Add the entity to the context
            _context.OrderRow.Add(entity);
            await _context.SaveChangesAsync();

            // Act
            var result = await repo.GetAsync(x => x.OrderId == 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Article2", result.ArticleNumber);
            Assert.Equal(3, result.Quantity);
        }
    }
}
