using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Utils;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.WPF.ViewModels
{
    public class DeleteUserViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly IServiceProvider _serviceProvider;

        public DeleteUserViewModel(IAuthService authService, IServiceProvider serviceProvider)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            DeleteUserCommand = new AsyncRelayCommand(DeleteUserAsync);
            GoBackCommand = new RelayCommand(GoBack);
        }

        private string _email;
        private string _firstName;
        private string _lastName;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public IAsyncRelayCommand DeleteUserCommand { get; }
        public IRelayCommand GoBackCommand { get; }

        private async Task DeleteUserAsync()
        {
            try
            {
                var success = await _authService.RemoveUserAsync(Email);

                if (success)
                {
                    Logger.Log($"User with email {Email} was successfully removed.", "DeleteUserViewModel.DeleteUserAsync()", LogTypes.Info);
                    MessageBox.Show($"User with email {Email} was successfully removed.");

                    Email = string.Empty;
                    FirstName = string.Empty;
                    LastName = string.Empty;

                    GoBack();
                }
                else
                {
                    Logger.Log($"Failed to remove user with email {Email}.", "DeleteUserViewModel.DeleteUserAsync()", LogTypes.Error);
                    MessageBox.Show($"An error occurred while removing the user.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "DeleteUserViewModel.DeleteUserAsync()", LogTypes.Error);
                MessageBox.Show($"An error occurred while removing the user: {ex.Message}");
            }
        }

        private void GoBack()
        {
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetService<MainOptionsViewModel>();
        }
    }
}
