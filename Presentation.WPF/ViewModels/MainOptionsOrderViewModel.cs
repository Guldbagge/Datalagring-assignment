using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace Presentation.WPF.ViewModels
{
    public class MainOptionsOrderViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        public MainOptionsOrderViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            GoBackCommand = new RelayCommand(GoBack);
        }

        public IRelayCommand GoBackCommand { get; }

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
