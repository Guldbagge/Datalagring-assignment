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
    public class RoleRepository_Tests
    {
        private readonly CustomerContext _context;

        public RoleRepository_Tests()
        {
            _context = new CustomerContext(new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_RoleEntity_To_Context_And_SaveChanges()
        {
            // Arrange
            var repo = new RoleRepository(_context);
            var entity = new RoleEntity
            {
                RoleName = "Admin"
            };

            // Act
            var result = await repo.CreateAsync(entity);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, _context.Roles);
        }

        [Fact]
        public async Task GetAsync_Should_Retrieve_RoleEntity_From_Context()
        {
            // Arrange
            var repo = new RoleRepository(_context);
            var entity = new RoleEntity
            {
                RoleName = "User"
            };

            _context.Roles.Add(entity);
            await _context.SaveChangesAsync();

            // Act
            var result = await repo.GetAsync(x => x.RoleName == "User");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User", result.RoleName);
        }

    }
}
