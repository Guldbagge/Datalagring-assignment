using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Dtos;
using Shared.Utils;
using System;
using System.Windows;

namespace Presentation.WPF.ViewModels
{
    public class GetOneProductViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly IServiceProvider _serviceProvider;

        public GetOneProductViewModel(IProductService productService, IServiceProvider serviceProvider)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _articleNumber = string.Empty;
            _title = string.Empty; 
            _description = string.Empty;

            GetProductCommand = new AsyncRelayCommand(GetProductAsync);
            GoBackCommand = new RelayCommand(GoBack);
        }

        private string _articleNumber;
        private string _title;
        private string _description;
        private decimal _price;

        public string ArticleNumber
        {
            get => _articleNumber;
            set => SetProperty(ref _articleNumber, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        public IAsyncRelayCommand GetProductCommand { get; }
        public RelayCommand GoBackCommand { get; }

        private async Task GetProductAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ArticleNumber))
                {
                    Logger.Log("Article number is empty.", "GetOneProductViewModel.GetProductAsync()", LogTypes.Warning);
                    MessageBox.Show("Please provide an article number.");
                    return;
                }

                var getOneProductDto = new GetOneProductDto { ArticleNumber = ArticleNumber };
                var product = await _productService.GetProductAsync(getOneProductDto);

                if (product != null)
                {
                    Title = product.Title;
                    Description = product.Description!;
                    Price = product.Price;

                    Logger.Log($"Product with article number {ArticleNumber} was retrieved successfully.", "GetOneProductViewModel.GetProductAsync()", LogTypes.Info);
                    MessageBox.Show($"Product with article number {ArticleNumber} was retrieved successfully.\nTitle: {Title}\nDescription: {Description}\nPrice: {Price}\n");
                }
                else
                {
                    Logger.Log($"Product with article number {ArticleNumber} was not found.", "GetOneProductViewModel.GetProductAsync()", LogTypes.Info);
                    MessageBox.Show($"Product with article number {ArticleNumber} was not found.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "GetOneProductViewModel.GetProductAsync()", LogTypes.Error);
            }
        }

        private void GoBack()
        {
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetService<MainOptionsViewModel>();
        }
    }
}
