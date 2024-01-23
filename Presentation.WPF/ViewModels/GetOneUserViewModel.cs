using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Utils;
using System.Windows;

namespace Presentation.WPF.ViewModels
{
    public class GetOneUserViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        public GetOneUserViewModel(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            GetUserCommand = new AsyncRelayCommand(GetUserAsync);
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

        public IAsyncRelayCommand GetUserCommand { get; }

        private async Task GetUserAsync()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Email))
                {
                    var user = await _authService.GetUserByEmailAsync(Email);

                    if (user != null)
                    {
                        FirstName = user.FirstName;
                        LastName = user.LastName;

                        Logger.Log($"User with email {Email} was retrieved successfully.", "GetOneUserViewModel.GetUserAsync()", LogTypes.Info);
                        MessageBox.Show($"User with email {Email} was retrieved successfully.\nName: {FirstName} {LastName}");
                    }
                    else
                    {
                        Logger.Log($"User with email {Email} was not found.", "GetOneUserViewModel.GetUserAsync()", LogTypes.Info);
                        MessageBox.Show($"User with email {Email} was not found.");
                    }
                }
                else
                {
                    Logger.Log("Email is empty.", "GetOneUserViewModel.GetUserAsync()", LogTypes.Warning);
                    MessageBox.Show("Please provide an email address.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "GetOneUserViewModel.GetUserAsync()", LogTypes.Error);
            }
        }
    }
}
