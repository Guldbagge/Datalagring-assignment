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
    public class BankSwishDetailsRepository_Tests
    {
        private readonly CustomerContext _context;

        public BankSwishDetailsRepository_Tests()
        {
            // Create a new in-memory database for each test
            _context = new CustomerContext(new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_BankSwishDetailsEntity_To_Context_And_SaveChanges()
        {
            // Arrange
            var repo = new BankSwishDetailsRepository(_context);
            var entity = new BankSwishDetailsEntity
            {
                UserId = 1,
                BankInformation = "Sample Bank Information",
                SwishInformation = "Sample Swish Information"
            };

            // Act
            var result = await repo.CreateAsync(entity);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, _context.BankSwishDetails);
        }

        [Fact]
        public async Task GetAsync_Should_Retrieve_BankSwishDetailsEntity_From_Context()
        {
            // Arrange
            var repo = new BankSwishDetailsRepository(_context);
            var entity = new BankSwishDetailsEntity
            {
                UserId = 2,
                BankInformation = "Sample Bank Information 2",
                SwishInformation = "Sample Swish Information 2"
            };

            // Add the entity to the context
            _context.BankSwishDetails.Add(entity);
            await _context.SaveChangesAsync();

            // Act
            var result = await repo.GetAsync(x => x.UserId == 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Sample Bank Information 2", result.BankInformation);
            Assert.Equal("Sample Swish Information 2", result.SwishInformation);
        }

    
    }
}
