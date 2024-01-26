using Infrastructure.Entities;
using Shared.Dtos;
using Shared.Utils;
using System;

public static class ProductFactory
{
    public static Product Create(AddProductDto addProductDto)
    {
        try
        {
            var productEntity = new Product
            {
                ArticleNumber = addProductDto.ArticleNumber,
                Title = addProductDto.Title,
                Description = addProductDto.Description,
                Price = addProductDto.Price
            };

            Logger.Log("ProductEntity created successfully.", "ProductFactory.Create", LogTypes.Info);
            return productEntity;
        }
        catch (Exception ex)
        {
            Logger.Log($"Error creating ProductEntity: {ex.Message}", "ProductFactory.Create", LogTypes.Error);
            return null!;
        }
    }

    public static AddProductDto Create(string articleNumber, string title, string description, decimal price, string categoryName)
    {
        try
        {
            var addProductDto = new AddProductDto
            {
                ArticleNumber = articleNumber,
                Title = title,
                Description = description,
                Price = price,
                CategoryName = categoryName
            };

            Logger.Log("AddProductDto created successfully.", "ProductFactory.Create", LogTypes.Info);
            return addProductDto;
        }
        catch (Exception ex)
        {
            Logger.Log($"Error creating AddProductDto: {ex.Message}", "ProductFactory.Create", LogTypes.Error);
            return null!;
        }
    }
}