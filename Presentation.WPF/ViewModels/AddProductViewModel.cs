using Business.Factories;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Presentation.WPF.Models;
using Shared.Dtos;
using Shared.Utils;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.WPF.ViewModels
{
    public partial class AddProductViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly IServiceProvider _serviceProvider;

        public AddProductViewModel(IServiceProvider serviceProvider, IProductService productService)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));

            GoBackCommand = new RelayCommand(GoBack);
        }

        [ObservableProperty]
        private AddProductModel _form = new();

        [RelayCommand]
        private async Task AddProduct()
        {
            try
            {
                var addProductDto = ProductFactory.Create(Form.ArticleNumber, Form.Title, Form.Description, Form.Price, Form.CategoryName);

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
            Logger.Log($"AddProductDto Information: ArticleNumber={addProductDto.ArticleNumber}, Title={addProductDto.Title}, Description={addProductDto.Description}, Price={addProductDto.Price}, CategoryName={addProductDto.CategoryName}", "AddProductViewModel.AddProduct()", LogTypes.Info);
        }

        private void GoBack()
        {
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetService<MainOptionsViewModel>();
        }

        public IRelayCommand GoBackCommand { get; }
    }
}
