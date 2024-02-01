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
    public class AuthRepository_Tests
    {
        private readonly CustomerContext _context;

        public AuthRepository_Tests()
        {
          
            _context = new CustomerContext(new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_AuthEntity_To_Context_And_SaveChanges()
        {
            // Arrange
            var repo = new AuthRepository(_context);
            var entity = new AuthEntity
            {
                UserId = 1,
                Password = "SamplePassword",
                RefreshToken = "SampleRefreshToken",
                AccessToken = "SampleAccessToken",
                LastSignedIn = DateTime.Now,
                IsPresistent = true
            };

            // Act
            var result = await repo.CreateAsync(entity);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, _context.Authentications);
        }

        [Fact]
        public async Task GetAsync_Should_Retrieve_AuthEntity_From_Context()
        {
            // Arrange
            var repo = new AuthRepository(_context);
            var entity = new AuthEntity
            {
                UserId = 2,
                Password = "AnotherPassword",
                RefreshToken = "AnotherRefreshToken",
                AccessToken = "AnotherAccessToken",
                LastSignedIn = DateTime.Now,
                IsPresistent = false
            };

            // Add the entity to the context
            _context.Authentications.Add(entity);
            await _context.SaveChangesAsync();

            // Act
            var result = await repo.GetAsync(x => x.UserId == 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AnotherPassword", result.Password);
            Assert.Equal("AnotherRefreshToken", result.RefreshToken);
            Assert.Equal("AnotherAccessToken", result.AccessToken);
            Assert.False(result.IsPresistent);
        }
    }
}
