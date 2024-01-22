//using Business.Dtos;
//using Business.Interfaces;
//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using Presentation.WPF.Models;
//using Shared.Utils;
//using System;
//using System.Collections.ObjectModel;
//using System.Windows;
//using System.Windows.Input;

//namespace Presentation.WPF.ViewModels
//{
//    public class GetAllDiversViewModel : ObservableObject
//    {
//        private readonly IAuthService _authService;

//        public GetAllDiversViewModel(IAuthService authService)
//        {
//            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
//            LoadDiversCommand = new RelayCommand(LoadDivers);
//            Divers = new ObservableCollection<GetAllDiversModel>();
//        }

//        public ICommand LoadDiversCommand { get; }

//        public ObservableCollection<GetAllDiversModel> Divers { get; set; }

//        private async void LoadDivers()
//        {
//            try
//            {
//                // Hämta alla dykare från databasen genom att anropa metoden i AuthService
//                var allDivers = await _authService.GetAllDiversAsync();

//                // Konvertera dykarobjekten till DiverViewModels om det behövs
//                foreach (var diver in allDivers)
//                {
//                    Divers.Add(new GetAllDiversModel(diver)); // Antag att du har en separat DiverViewModel för att representera varje dykare.
//                }

//                Logger.Log("Divers were loaded successfully.", "GetAllDiversViewModel.LoadDivers()", LogTypes.Info);
//                MessageBox.Show("Divers were loaded successfully.");
//            }
//            catch (Exception ex)
//            {
//                Logger.Log(ex.Message, "GetAllDiversViewModel.LoadDivers()", LogTypes.Error);
//                MessageBox.Show("Something went wrong while loading divers.");
//            }
//        }
//    }
//}




//using Business.Factories;
//using Business.Interfaces;
//using Business.Services;
//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using Presentation.WPF.Models;
//using Shared.Utils;
//using System.Collections.ObjectModel;
//using System.Windows;

//namespace Presentation.WPF.ViewModels;


//public partial class GetAllDiversViewModel(IAuthService authService) : ObservableObject
//{
//    private readonly IAuthService _authService = authService;


//    [ObservableProperty]
//    private GetAllDiversModel _form = new();

//    [RelayCommand]
//    private async Task GetAllDivers()
//    {
//        try
//        {
//            var getAllDiversDto = GetAllDiversDtoFactory.Create(Form.FirstName, Form.LastName, Form.Email);
//            Form = new();

//            var result = await _authService.GetAllDiversAsync(getAllDiversDto);
//            if (result)
//            {
//                Logger.Log("User was created successfully.", "GetAllDiversViewModel.GetAllDivers()", LogTypes.Info);
//                MessageBox.Show("User was created successfully.");
//            }
//            else
//            {
//                Logger.Log("User was not created successfully.", "GetAllDiversViewModel.GetAllDivers()", LogTypes.Info);
//                MessageBox.Show("Something went wrong.");
//            }
//        }
//        catch (Exception ex) { Logger.Log(ex.Message, "GetAllDiversViewModel.GetAllDivers()", LogTypes.Error); }
//    }
//}
