using Infrastructure.Entities;
using Shared.Dtos;

namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SignUpAsync(SignUpDto signUpDto);
        Task<UserEntity> GetUserByEmailAsync(string email); // Lägg till denna metod
    }
}