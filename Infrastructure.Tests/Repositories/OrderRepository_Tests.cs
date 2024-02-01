using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests.Repositories
{
    public class OrderRepository_Tests
    {
        private readonly CustomerContext _context;

        public OrderRepository_Tests()
        {
            _context = new CustomerContext(new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_OrderEntity_To_Context_And_SaveChanges()
        {
            // Arrange
            var repo = new OrderRepository(_context);
            var entity = new OrderEntity
            {
                UserId = 1,
                OrderDate = DateTime.Now,
                User = new UserEntity { Id = 1, FirstName = "Sample", LastName = "User", Email = "sample@example.com", AcceptsUserTerms = true, AcceptsMarketingTerms = true, RoleId = 1 },
                OrderRows = { new OrderRowEntity { ArticleNumber = "Article1", Quantity = 2 } }
            };

            // Act
            var result = await repo.CreateAsync(entity);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, _context.Order); 
        }

        [Fact]
        public async Task GetAsync_Should_Retrieve_OrderEntity_From_Context()
        {
            // Arrange
            var repo = new OrderRepository(_context);
            var entity = new OrderEntity
            {
                UserId = 2,
                OrderDate = DateTime.Now,
                User = new UserEntity { Id = 2, FirstName = "Another", LastName = "User", Email = "another@example.com", AcceptsUserTerms = true, AcceptsMarketingTerms = false, RoleId = 2 },
                OrderRows = { new OrderRowEntity { ArticleNumber = "Article2", Quantity = 3 } }
            };

            // Add the entity to the context
            _context.Order.Add(entity); 
            await _context.SaveChangesAsync();

            // Act
            var result = await repo.GetAsync(x => x.UserId == 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Article2", result.OrderRows.First().ArticleNumber);
            Assert.Equal(3, result.OrderRows.First().Quantity);
        }
    }
}
