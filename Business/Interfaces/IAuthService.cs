using Infrastructure.Entities;
using Shared.Dtos;

namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SignUpAsync(SignUpDto signUpDto);
        Task<UserEntity> GetUserByEmailAsync(string email);
        Task<bool> RemoveUserAsync(string email);
        Task<List<UserEntity>> GetAllUsersAsync();
    }

}