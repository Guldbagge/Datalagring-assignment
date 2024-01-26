// IAuthService.cs
using Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace Business.Interfaces;

public interface IAuthService
{
    Task<bool> SignUpAsync(SignUpDto signUpDto);
    Task<UserEntity> GetUserByEmailAsync(GetOneUserDto getUserDto);
    Task<bool> RemoveUserAsync(string email);
    Task<List<UserEntity>> GetAllUsersAsync();
    Task<bool> UpdateUserAsync(UpdateUserDto updateUserDto);
   
}
