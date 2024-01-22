using Business.Factories;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.WPF.Models;
using Shared.Utils;
using System.Windows;

namespace Presentation.WPF.ViewModels;

public partial class SignUpViewModel(IAuthService authService) : ObservableObject
{
    private readonly IAuthService _authService = authService;

    [ObservableProperty]
    private SignUpFormModel _form = new();

    [RelayCommand]
    private async Task SignUp()
    {
        try
        {
            var signUpDto = SignUpDtoFactory.Create(Form.FirstName, Form.LastName, Form.Email, Form.Password, Form.AcceptsUserTerms, Form.AcceptsMarketingTerms);
            Form = new();
            
            var result = await _authService.SignUpAsync(signUpDto);
            if (result)
            {
                Logger.Log("User was created successfully.", "SignUpViewModel.SignUp()", LogTypes.Info);
                MessageBox.Show("User was created successfully.");
            }
            else
            {
                Logger.Log("User was not created successfully.", "SignUpViewModel.SignUp()", LogTypes.Info);
                MessageBox.Show("Something went wrong.");
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "SignUpViewModel.SignUp()", LogTypes.Error); }
    }
}
