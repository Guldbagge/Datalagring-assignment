using Infrastructure.Entities;
using Shared.Utils;
using System;

public static class CategoryFactory
{
    public static Category Create()
    {
        try
        {
            var category = new Category();

            Logger.Log("Category created successfully.", "CategoryFactory.Create", LogTypes.Info);
            return category;
        }
        catch (Exception ex)
        {
            Logger.Log($"Error creating Category: {ex.Message}", "CategoryFactory.Create", LogTypes.Error);
            return null!;
        }
    }

    public static Category Create(string categoryName)
    {
        try
        {
            var category = new Category
            {
                CategoryName = categoryName
            };

            Logger.Log("Category created successfully.", "CategoryFactory.Create", LogTypes.Info);
            return category;
        }
        catch (Exception ex)
        {
            Logger.Log($"Error creating Category: {ex.Message}", "CategoryFactory.Create", LogTypes.Error);
            return null!;
        }
    }
}