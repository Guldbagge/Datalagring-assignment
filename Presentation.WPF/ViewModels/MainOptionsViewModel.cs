using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.ViewModels;

public partial class MainOptionsViewModel : ObservableObject
{
    private readonly IServiceProvider _sp;

    public MainOptionsViewModel(IServiceProvider sp)
    {
        _sp = sp;
    }

    [RelayCommand]
    private void NavigateToSignUp()
    {
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();

        if (mainViewModel != null)
        {
            mainViewModel.CurrentViewModel = _sp.GetRequiredService<SignUpViewModel>();
        }
        else
        {
            Debug.WriteLine("MainViewModel is null. Unable to set CurrentViewModel.");
        }
    }

    [RelayCommand]
    private void NavigateToGetAllUser()
    {
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();

        if (mainViewModel != null)
        {
            mainViewModel.CurrentViewModel = _sp.GetRequiredService<GetAllUserViewModel>();
        }
        else
        {
            Debug.WriteLine("MainViewModel is null. Unable to set CurrentViewModel.");
        }
    }

    [RelayCommand]
    private void NavigateToGetUserDetails()
    {
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();

        if (mainViewModel != null)
        {
            mainViewModel.CurrentViewModel = _sp.GetRequiredService<GetOneUserViewModel>();
        }
        else
        {
            Debug.WriteLine("MainViewModel is null. Unable to set CurrentViewModel.");
        }
    }
}
