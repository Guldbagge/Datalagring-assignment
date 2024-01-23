using CommunityToolkit.Mvvm.ComponentModel;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Shared.Utils;

namespace Presentation.WPF.ViewModels
{
    public class GetAllUserViewModel : ObservableObject
    {
        private readonly IUserRepository _userRepository;
        private readonly IServiceProvider _serviceProvider;

        public GetAllUserViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            LoadUsersAsync();
        }

        private async void LoadUsersAsync()
        {
            try
            {
                Users = await _userRepository.GetAllAsync();
                Logger.Log("All users were retrieved successfully.", "GetAllUserViewModel.LoadUsersAsync()", LogTypes.Info);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "GetAllUserViewModel.LoadUsersAsync()", LogTypes.Error);
            }
        }

        private List<UserEntity> _users;

        public List<UserEntity> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

    }
}
