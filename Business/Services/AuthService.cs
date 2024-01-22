using Business.Factories;
using Business.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Shared.Dtos;
using Shared.Utils;

namespace Business.Services;

public class AuthService(IUserRepository userRepository, IAuthRepository authRepository, IRoleRepository roleRepository) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAuthRepository _authRepository = authRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;


    public async Task<bool> SignUpAsync(SignUpDto signUpDto)
    {
        try
        {
            if (! await CheckIfUserExistsAsync(signUpDto.Email))
            {
                var (userEntity, authEntity) = UserFactory.Create(signUpDto);
                
                if (userEntity != null && authEntity != null)
                {
                    var roleEntity = await _roleRepository.GetAsync(x => x.RoleName == "Standard User");
                    if (roleEntity != null)
                    {
                        userEntity.RoleId = roleEntity.Id;
                        var result = await CreateUserAsync(userEntity, authEntity);
                        return result;
                    }
                }
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "AuthService.SignUpAsync()", LogTypes.Error); }
        return false;
    }



    public async Task<bool> CheckIfUserExistsAsync(string email)
    {
        if (await _userRepository.ExistsAsync(x => x.Email == email))
        {
            Logger.Log($"User with {email} already exists.", "AuthService.SignUpAsync()", LogTypes.Info);
            return true;
        }

        return false;
    }

    public async Task<bool> CreateUserAsync(UserEntity userEntity, AuthEntity authEntity)
    {
        userEntity = await _userRepository.CreateAsync(userEntity, nameof(_userRepository));
        if (userEntity != null)
        {
            if (userEntity.Id > 0)
            {
                authEntity.UserId = userEntity.Id;

                var result = await _authRepository.CreateAsync(authEntity, nameof(_userRepository));
                if (result != null)
                {
                    Logger.Log("A user was created successfully.", "AuthService.SignUpAsync()", LogTypes.Info);
                    return true;
                }
            }
        }

        return false;
    }
}
