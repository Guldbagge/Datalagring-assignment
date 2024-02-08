using Business.Factories;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Presentation.WPF.Models;
using Shared.Dtos;
using Shared.Utils;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.WPF.ViewModels
{
    public partial class UpdateUserViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly IServiceProvider _serviceProvider;

        public UpdateUserViewModel(IServiceProvider serviceProvider, IAuthService authService)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));

            GoBackCommand = new RelayCommand(GoBack);
            GetUserCommand = new AsyncRelayCommand(GetUserAsync);
        }

        [ObservableProperty]
        private UpdateUserFormModel _form = new UpdateUserFormModel();

        private int _userId;
        private string _firstName = "";
        private string _lastName = "";
        private string _email = "";

        public string CombinedFirstName => $"{FirstName} {Form.FirstName}";

        public string CombinedLastName => $"{LastName} {Form.LastName}";

        public string CombinedEmail => $"{Email} {Form.Email}";

        public string CombinedPassword => $"{Form.Password}";

        public string CombinedConfirmPassword => $"{Form.ConfirmPassword}";


        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
                Form.Id = value;
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(CombinedFirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(CombinedLastName));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(CombinedEmail));
            }
        }

        private string _editableEmail = "";
        private string _editableFirstName = "";
        private string _editableLastName = "";
        private string _editablePassword = "";
        private string _editableConfirmPassword = "";

        public string EditableFirstName
        {
            get => _editableFirstName;
            set
            {
                _editableFirstName = value;
                OnPropertyChanged(nameof(EditableFirstName));
            }
        }

        public string EditableLastName
        {
            get => _editableLastName;
            set
            {
                _editableLastName = value;
                OnPropertyChanged(nameof(EditableLastName));
            }
        }

        public string EditableEmail
        {
            get => _editableEmail;
            set
            {
                _editableEmail = value;
                OnPropertyChanged(nameof(EditableEmail));
            }
        }

        public string EditablePassword
        {
            get => _editablePassword;
            set
            {
                _editablePassword = value;
                OnPropertyChanged(nameof(EditablePassword));
            }
        }

        public string EditableConfirmPassword
        {
            get => _editableConfirmPassword;
            set
            {
                _editableConfirmPassword = value;
                OnPropertyChanged(nameof(EditableConfirmPassword));
            }
        }

        private async Task GetUserAsync()
        {
            try
            {
                if (UserId > 0)
                {
                    var user = await _authService.GetUserByIdAsync(UserId);

                    if (user != null)
                    {
                        FirstName = user.FirstName;
                        LastName = user.LastName;
                        Email = user.Email;
                        EditableFirstName = user.FirstName;
                        EditableLastName = user.LastName;
                        EditableEmail = user.Email;

                        Logger.Log($"User with ID {UserId} was retrieved successfully.", "UpdateUserViewModel.GetUserAsync()", LogTypes.Info);
                        MessageBox.Show($"User with ID {UserId} was retrieved successfully.\nName: {FirstName} {LastName}");
                    }
                    else
                    {
                        Logger.Log($"User with ID {UserId} was not found.", "UpdateUserViewModel.GetUserAsync()", LogTypes.Info);
                        MessageBox.Show($"User with ID {UserId} was not found.");
                    }
                }
                else
                {
                    Logger.Log("User ID is invalid.", "UpdateUserViewModel.GetUserAsync()", LogTypes.Warning);
                    MessageBox.Show("Please provide a valid user ID.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "UpdateUserViewModel.GetUserAsync()", LogTypes.Error);
            }
        }

        [RelayCommand]
        private async Task UpdateUser()
        {
            try
            {
                if (int.TryParse(Form.Id.ToString(), out int userId))
                {
                    Logger.Log($"User ID to update: {userId}", "UpdateUserViewModel.UpdateUser()", LogTypes.Info);

                    var updateUserDto = UpdateUserDtoFactory.Create(userId, EditableFirstName, EditableLastName, EditableEmail, EditablePassword);
                    Form = new UpdateUserFormModel();

                    var result = await _authService.UpdateUserAsync(updateUserDto);
                    if (result)
                    {
                        Logger.Log("User was updated successfully.", "UpdateUserViewModel.UpdateUser()", LogTypes.Info);
                        MessageBox.Show("User was updated successfully.");
                    }
                    else
                    {
                        Logger.Log("User was not updated successfully.", "UpdateUserViewModel.UpdateUser()", LogTypes.Info);
                        MessageBox.Show("Something went wrong.");
                    }
                }
                else
                {
                    Logger.Log("Invalid user ID.", "UpdateUserViewModel.UpdateUser()", LogTypes.Error);
                    MessageBox.Show("Invalid user ID.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "UpdateUserViewModel.UpdateUser()", LogTypes.Error);
            }
        }

        private void GoBack()
        {
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetService<MainOptionsViewModel>();
        }

        public IRelayCommand GoBackCommand { get; }
        public AsyncRelayCommand GetUserCommand { get; }
    }
}
