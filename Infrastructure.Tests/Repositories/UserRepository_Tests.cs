using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests.Repositories
{
    public class UserRepository_Tests
    {
        private readonly CustomerContext _context;

        public UserRepository_Tests()
        {
            _context = new CustomerContext(new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options);
        }

        [Fact]
        public async Task GetAsync_Should_Retrieve_UserEntity_With_Role_From_Context()
        {
            // Arrange
            var repo = new UserRepository(_context);
            var role = new RoleEntity { RoleName = "User" };
            var user = new UserEntity
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                AcceptsUserTerms = true,
                AcceptsMarketingTerms = false,
                Role = role
            };

            _context.Roles.Add(role);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await repo.GetAsync(x => x.Email == "john.doe@example.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("john.doe@example.com", result.Email);
            Assert.True(result.AcceptsUserTerms);
            Assert.False(result.AcceptsMarketingTerms);
            Assert.NotNull(result.Role);
            Assert.Equal("User", result.Role.RoleName);
        }

        [Fact]
        public async Task GetAllAsync_Should_Retrieve_All_UserEntities_With_Roles_From_Context()
        {
            // Arrange
            var repo = new UserRepository(_context);
            var role1 = new RoleEntity { RoleName = "Admin" };
            var role2 = new RoleEntity { RoleName = "User" };
            var user1 = new UserEntity
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                AcceptsUserTerms = true,
                AcceptsMarketingTerms = true,
                Role = role1
            };
            var user2 = new UserEntity
            {
                FirstName = "Bob",
                LastName = "Smith",
                Email = "bob.smith@example.com",
                AcceptsUserTerms = true,
                AcceptsMarketingTerms = false,
                Role = role2
            };

            // Lägg till entiteterna i kontexten
            _context.Roles.AddRange(role1, role2);
            _context.Users.AddRange(user1, user2);
            await _context.SaveChangesAsync();

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, u => u.Email == "jane.doe@example.com");
            Assert.Contains(result, u => u.Email == "bob.smith@example.com");
            foreach (var user in result)
            {
                Assert.NotNull(user.Role);
            }
        }
    }
}
