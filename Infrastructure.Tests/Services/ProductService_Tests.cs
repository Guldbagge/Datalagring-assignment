
using Business.Factories;
using Business.Interfaces;
using Business.Services;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Moq;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests.Services;

public class ProductService_Tests
{
    private readonly ProductService _productService;
    private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock = new Mock<ICategoryRepository>();

    public ProductService_Tests()
    {
        _productService = new ProductService(_productRepositoryMock.Object, _categoryRepositoryMock.Object);
    }

    [Fact]
    public async Task AddProductAsync_ShouldReturnTrue_WhenProductIsAdded()
    {
        // Arrange
        var addProductDto = new AddProductDto
        {
            ArticleNumber = "12345",
            Title = "TestProduct",
            Description = "Sample Description",
            Price = 19.99M,
            CategoryName = "TestCategory"
        };

        _productRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(false);
        _categoryRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(new Category { Id = 1 });
        _productRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Product>(), It.IsAny<string>())).ReturnsAsync(new Product { ArticleNumber = "12345" });

        // Act
        var result = await _productService.AddProductAsync(addProductDto);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetProductAsync_ShouldReturnProductDto_WhenProductExists()
    {
        // Arrange
        var articleNumber = "12345";

        _productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
            .ReturnsAsync(new Product
            {
                ArticleNumber = articleNumber,
                Title = "TestProduct",
                Description = "Sample Description",
                Price = 19.99M,
                Category = new Category { Id = 1, CategoryName = "TestCategory" }
            });

        // Act
        var result = await _productService.GetProductAsync(articleNumber);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(articleNumber, result.ArticleNumber);
    }

    [Fact]
    public async Task GetAllProductsAsync_ShouldReturnProductDtos_WhenProductsExist()
    {
        // Arrange
        var productEntities = new List<Product>
        {
            new Product { ArticleNumber = "12345", Title = "Product1", Price = 19.99M, Category = new Category { Id = 1, CategoryName = "TestCategory" } },
            new Product { ArticleNumber = "67890", Title = "Product2", Price = 29.99M, Category = new Category { Id = 2, CategoryName = "TestCategory" } }
        };

        _productRepositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(productEntities);

        // Act
        var result = await _productService.GetAllProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productEntities.Count, result.Count());
    }
}
