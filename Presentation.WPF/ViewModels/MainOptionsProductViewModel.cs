using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace Presentation.WPF.ViewModels
{
    public partial class MainOptionsProductViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        public MainOptionsProductViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            GoBackCommand = new RelayCommand(GoBack);
        }

        public IRelayCommand GoBackCommand { get; }

        [RelayCommand]
        private void NavigateToAddProduct()
        {
            try
            {
                var mainViewModel = _serviceProvider.GetService<MainViewModel>();

                if (mainViewModel != null)
                {
                    mainViewModel.CurrentViewModel = _serviceProvider.GetService<AddProductViewModel>();
                }
                else
                {
                    Debug.WriteLine("MainViewModel is null. Unable to set CurrentViewModel.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while navigating to AddProductViewModel: {ex.Message}");
            }
        }

        [RelayCommand]
        private void NavigateToGetOneProduct()
        {
            try
            {
                var mainViewModel = _serviceProvider.GetService<MainViewModel>();

                if (mainViewModel != null)
                {
                    mainViewModel.CurrentViewModel = _serviceProvider.GetService<GetOneProductViewModel>();
                }
                else
                {
                    Debug.WriteLine("MainViewModel is null. Unable to set CurrentViewModel.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while navigating to GetAllProductViewModel: {ex.Message}");
            }
        }

        private void GoBack()
        {
            try
            {
                var mainViewModel = _serviceProvider.GetService<MainViewModel>();
                mainViewModel.CurrentViewModel = _serviceProvider.GetService<MainOptionsViewModel>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while navigating back: {ex.Message}");
            }
        }
    }
}
