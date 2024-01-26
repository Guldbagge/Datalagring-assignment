
using Business.Factories;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Presentation.WPF.Models;
using Shared.Dtos;
using Shared.Utils;
using System;
using System.Windows;

namespace Presentation.WPF.ViewModels
{
    public partial class AddProductViewModel(IProductService productService) : ObservableObject
    {
        private readonly IProductService _productService = productService;

        [ObservableProperty]
        private AddProductModel _form = new();

        [RelayCommand]
        private async Task AddProduct()
        {
            try
            {
                var addProductDto = ProductFactory.Create(Form.ArticleNumber, Form.Title, Form.Description, Form.Price, Form.CategoryName);

                // Log the information for debugging
                LogAddProductDtoInfo(addProductDto);

                Form = new();

                Logger.Log("Before calling AddProductAsync", "AddProductViewModel.AddProduct()", LogTypes.Info);

                var result = await _productService.AddProductAsync(addProductDto);

                Logger.Log($"After calling AddProductAsync, result: {result}", "AddProductViewModel.AddProduct()", LogTypes.Info);

                if (result)
                {
                    Logger.Log("Product was created successfully.", "AddProductViewModel.AddProduct()", LogTypes.Info);
                    MessageBox.Show("Product was created successfully.");
                }
                else
                {
                    Logger.Log("Product was not created successfully.", "AddProductViewModel.AddProduct()", LogTypes.Info);
                    MessageBox.Show("Something went wrong.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "AddProductViewModel.AddProduct()", LogTypes.Error);
            }
        }

        private void LogAddProductDtoInfo(AddProductDto addProductDto)
        {
            // Logga informationen från addProductDto för felsökning
            Logger.Log($"AddProductDto Information: ArticleNumber={addProductDto.ArticleNumber}, Title={addProductDto.Title}, Description={addProductDto.Description}, Price={addProductDto.Price}, CategoryName={addProductDto.CategoryName}", "AddProductViewModel.AddProduct()", LogTypes.Info);
        }
    }
}



//using Business.Factories;
//using Business.Interfaces;
//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using Microsoft.Extensions.Logging;
//using Presentation.WPF.Models;
//using Shared.Utils;
//using System;
//using System.Windows;

//namespace Presentation.WPF.ViewModels
//{
//    public partial class AddProductViewModel(IProductService productService) : ObservableObject
//    {
//        private readonly IProductService _productService = productService;

//        [ObservableProperty]
//        private AddProductModel _form = new ();

//        [RelayCommand]
//        private async Task AddProduct()
//        {
//            try
//            {
//                var addProductDto = ProductFactory.Create(Form.ArticleNumber, Form.Title, Form.Description, Form.Price, Form.CategoryName);

//                Form = new();

//                var result = await _productService.AddProductAsync(addProductDto);
//                if (result)
//                {
//                    Logger.Log("Product was created successfully.", "AddProductViewModel.AddProduct()", LogTypes.Info);
//                    MessageBox.Show("Product was created successfully.");
//                }
//                else
//                {
//                    Logger.Log("Product was not created successfully.", "AddProductViewModel.AddProduct()", LogTypes.Info);
//                    MessageBox.Show("Something went wrong.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.Log(ex.Message, "AddProductViewModel.AddProduct()", LogTypes.Error);
//            }
//        }
//    }
//}
