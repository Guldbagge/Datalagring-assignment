//using Shared.Dtos;
//using Shared.Utils;


//namespace Business.Factories;

//public static class AddProductDtoFactory
//{
//    public static AddProductDto Create(string articleNumber, string title, string description, decimal price)
//    {
//        try
//        {
//            var addProductDto = new AddProductDto
//            {
//                ArticleNumber = articleNumber,
//                Title = title,
//                Description = description,
//                Price = price
//            };

//            return addProductDto;

//        }
//        catch (Exception ex) { Logger.Log(ex.Message, "AddProductDtoFactory.Create()", LogTypes.Error); }

//        return null!;
//    }
//}
