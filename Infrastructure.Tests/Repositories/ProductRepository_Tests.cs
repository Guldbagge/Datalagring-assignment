using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests.Repositories;


public class ProductRepository_Tests
{
    private readonly ProductCatalogContext _context;

    public ProductRepository_Tests()
    {
        var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

        _context = new ProductCatalogContext(new DbContextOptionsBuilder<ProductCatalogContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .UseInternalServiceProvider(serviceProvider)
            .Options);
    }

    [Fact]
    public async Task CreateAsync_Should_Add_Product_To_Context_And_SaveChanges()
    {
        // Arrange
        var repo = new ProductRepository(_context);
        var product = new Product
        {
            ArticleNumber = "Article1",
            Title = "Product1",
            Description = "Sample Description",
            Price = 10.99M,
            CategoryId = 1
        };

        // Act
        var result = await repo.CreateAsync(product, nameof(CreateAsync_Should_Add_Product_To_Context_And_SaveChanges));

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, _context.Products);
    }

    //[Fact]
    //public async Task GetWithCategoryAsync_Should_Retrieve_Product_With_Category_Based_on_Predicate_From_Context()
    //{
    //    // Arrange
    //    var repo = new ProductRepository(_context);
    //    var product = new Product
    //    {
    //        ArticleNumber = "Article1",
    //        Title = "Product1",
    //        Description = "Sample Description",
    //        Price = 10.99M,
    //        CategoryId = 1
    //    };

    //    _context.Products.Add(product);
    //    await _context.SaveChangesAsync();

    //    Expression<Func<Product, bool>> predicate = x => x.ArticleNumber == "Article1";

    //    // Act
    //    var result = await repo.GetWithCategoryAsync(predicate);

    //    // Log for debugging
    //    Console.WriteLine($"Result from repository: {result?.ArticleNumber}");

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Equal("Article1", result.ArticleNumber);
    //}
}
