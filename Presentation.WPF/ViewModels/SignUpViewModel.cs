using Business.Factories;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Presentation.WPF.Models;
using Shared.Utils;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.WPF.ViewModels
{
    public partial class SignUpViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly IServiceProvider _serviceProvider;

        public SignUpViewModel(IServiceProvider serviceProvider, IAuthService authService)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));

            GoBackCommand = new RelayCommand(GoBack);
        }

        [ObservableProperty]
        private SignUpFormModel _form = new();

        [RelayCommand]
        private async Task SignUp()
        {
            try
            {
                var signUpDto = SignUpDtoFactory.Create(Form.FirstName, Form.LastName, Form.Email, Form.Password, Form.AcceptsUserTerms, Form.AcceptsMarketingTerms);
                Form = new();

                Logger.Log("Before calling SignUpAsync", "SignUpViewModel.SignUp()", LogTypes.Info);

                var userExists = await _authService.CheckIfUserExistsAsync(signUpDto.Email);

                if (userExists)
                {
                    Logger.Log($"User with email {signUpDto.Email} already exists.", "SignUpViewModel.SignUp()", LogTypes.Info);
                    MessageBox.Show($"User with email {signUpDto.Email} already exists.");
                }
                else
                {
                    var result = await _authService.SignUpAsync(signUpDto);

                    Logger.Log($"After calling SignUpAsync, result: {result}", "SignUpViewModel.SignUp()", LogTypes.Info);

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
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "SignUpViewModel.SignUp()", LogTypes.Error);
            }
        }


        private void GoBack()
        {
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetService<MainOptionsViewModel>();
        }

        public IRelayCommand GoBackCommand { get; }
    }
}
