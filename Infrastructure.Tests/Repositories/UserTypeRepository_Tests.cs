using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests.Repositories
{
    public class UserTypeRepository_Tests
    {
        private readonly CustomerContext _context;

        public UserTypeRepository_Tests()
        {
            _context = new CustomerContext(new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options);
        }

        [Fact]
        public async Task GetAsync_Should_Return_Null_When_No_UserTypeEntity_Found()
        {
            // Arrange
            var repo = new UserTypeRepository(_context);

            // Act
            var result = await repo.GetAsync(1);

            // Assert
            Assert.Null(result);
        }
    }
}
