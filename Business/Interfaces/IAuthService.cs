using Shared.Dtos;

namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SignUpAsync(SignUpDto signUpDto);
    }
}