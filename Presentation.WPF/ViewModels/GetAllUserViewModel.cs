using CommunityToolkit.Mvvm.ComponentModel;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.WPF.ViewModels
{
    public class GetAllUserViewModel : ObservableObject
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _ = LoadUsersAsync();
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
    }
}
