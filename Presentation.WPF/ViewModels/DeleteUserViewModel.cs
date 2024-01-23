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
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return _lastName; }
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
                // Anropa AuthService eller annan lämplig tjänst för att ta bort användaren
                // Du kan använda _authService eller annan lämplig tjänst för att genomföra borttagningen.

                var success = await _authService.RemoveUserAsync(Email);

                if (success)
                {
                    Logger.Log($"User with email {Email} was successfully removed.", "DeleteUserViewModel.DeleteUserAsync()", LogTypes.Info);
                    MessageBox.Show($"User with email {Email} was successfully removed.");

                    // Efter borttagningen kanske du vill nollställa dina egenskaper, eller annan lämplig logik.
                    Email = string.Empty;
                    FirstName = string.Empty;
                    LastName = string.Empty;

                    // Gå tillbaka till huvudmenyn
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
