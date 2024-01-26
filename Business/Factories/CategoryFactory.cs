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
                // Du kan eventuellt sätta andra egenskaper här om det behövs
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

    // Eventuellt kan du lägga till fler metoder för att skapa Category-instanser med olika parametrar här.
}


//using Infrastructure.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Business.Factories;

//public static class CategoryFactory
//{
//    public static Category Create()
//    {
//        try
//        {
//            var category = new Category();

//            return category;

//        }
//        catch (Exception ex)
//        {
//            // Logga ett fel om något går fel vid skapandet
//            Console.WriteLine($"Error in CategoryFactory.Create(): {ex.Message}");
//            return null!;
//        }
//    }

//    public static Category Create(string categoryName)
//    {
//        try
//        {
//            var category = new Category
//            {
//                CategoryName = categoryName
//                // Du kan eventuellt sätta andra egenskaper här om det behövs
//            };

//            return category;
//        }
//        catch (Exception ex)
//        {
//            // Logga ett fel om något går fel vid skapandet
//            Console.WriteLine($"Error in CategoryFactory.Create(): {ex.Message}");
//            return null!;
//        }
//    }

//    // Eventuellt kan du lägga till fler metoder för att skapa Category-instanser med olika parametrar här.
//}
