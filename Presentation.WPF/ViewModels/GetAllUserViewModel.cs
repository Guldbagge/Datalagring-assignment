using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.WPF.ViewModels;

public class GetAllUserViewModel : ObservableObject
{
    private readonly IUserRepository _userRepository;
    private readonly IServiceProvider _serviceProvider;

    public GetAllUserViewModel(IServiceProvider serviceProvider, IUserRepository userRepository)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _users = new List<UserEntity>();

        _ = LoadUsersAsync();

        GoBackCommand = new RelayCommand(GoBack);
    }

    private async Task LoadUsersAsync()
    {
        try
        {
            Users = await _userRepository.GetAllAsync();
            ShowMessageToUser("The entire user list has been retrieved successfully.");
        }
        catch (Exception ex)
        {
            Logger.Log(ex.Message, "GetAllUserViewModel.LoadUsersAsync()", LogTypes.Error);
            ShowMessageToUser("An error occurred while retrieving the user list. Please try again.");
        }
    }

    private void ShowMessageToUser(string message)
    {
        MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private List<UserEntity> _users;

    public List<UserEntity> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged(nameof(Users));
        }
    }

    private void GoBack()
    {
        var mainViewModel = _serviceProvider.GetService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetService<MainOptionsViewModel>();
    }

    public IRelayCommand GoBackCommand { get; }
}
