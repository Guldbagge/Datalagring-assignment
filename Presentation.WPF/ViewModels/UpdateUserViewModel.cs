using Business.Factories;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.WPF.Models;
using Shared.Utils;
using System.Windows;

namespace Presentation.WPF.ViewModels
{
    public partial class UpdateUserViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private UpdateUserFormModel _form = new UpdateUserFormModel();

        public UpdateUserViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task UpdateUser()
        {
            try
            {
                if (int.TryParse(Form.Id.ToString(), out int userId))
                {
                    Logger.Log($"User ID to update: {userId}", "UpdateUserViewModel.UpdateUser()", LogTypes.Info);

                    var updateUserDto = UpdateUserDtoFactory.Create(userId, Form.FirstName, Form.LastName, Form.Email, Form.Password);
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
    }
}
