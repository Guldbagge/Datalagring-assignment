using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace Presentation.WPF.ViewModels;

public partial class MainOptionsOrderViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    public MainOptionsOrderViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        GoBackCommand = new RelayCommand(GoBack);
       
    }

    public IRelayCommand GoBackCommand { get; }

    [RelayCommand]
    private void NavigateToAddOrder()
    {
        try
        {
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();

            if (mainViewModel != null)
            {
                mainViewModel.CurrentViewModel = _serviceProvider.GetService<AddOrderViewModel>();
            }
            else
            {
                Debug.WriteLine("MainViewModel is null. Unable to set CurrentViewModel.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred while navigating to AddOrderViewModel: {ex.Message}");
        }
    }

    [RelayCommand]
    private void NavigateToGetOneOrder()
    {
        try
        {
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();

            if (mainViewModel != null)
            {
                mainViewModel.CurrentViewModel = _serviceProvider.GetService<GetOneOrderViewModel>();
            }
            else
            {
                Debug.WriteLine("MainViewModel is null. Unable to set CurrentViewModel.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred while navigating to GetOneOrderViewModel: {ex.Message}");
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
