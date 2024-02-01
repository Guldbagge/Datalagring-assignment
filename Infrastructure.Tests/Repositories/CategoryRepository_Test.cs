using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests.Repositories
{
    public class CategoryRepository_Tests
    {
        private readonly ProductCatalogContext _context;

        public CategoryRepository_Tests()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new in-memory database for each test
            _context = new ProductCatalogContext(new DbContextOptionsBuilder<ProductCatalogContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .UseInternalServiceProvider(serviceProvider)
                .Options);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_Category_To_Context_And_SaveChanges()
        {
            // Arrange
            var repo = new CategoryRepository(_context);
            var category = new Category
            {
                CategoryName = "SampleCategory"
            };

            // Act
            var result = await repo.CreateAsync(category);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, _context.Categories);
        }

        [Fact]
        public async Task GetAsync_Should_Retrieve_Category_From_Context()
        {
            // Arrange
            var repo = new CategoryRepository(_context);
            var category = new Category
            {
                CategoryName = "AnotherCategory"
            };

            // Add the category to the context
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Act
            var result = await repo.GetAsync(x => x.CategoryName == "AnotherCategory");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AnotherCategory", result.CategoryName);
        }
    }
}
