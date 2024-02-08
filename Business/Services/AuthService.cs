﻿using Business.Factories;
using Business.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Shared.Dtos;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IUserRepository userRepository, IAuthRepository authRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<bool> SignUpAsync(SignUpDto signUpDto)
        {
            try
            {
                if (!await CheckIfUserExistsAsync(signUpDto.Email))
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
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "AuthService.SignUpAsync()", LogTypes.Error);
            }
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

        public async Task<UserEntity> GetUserByEmailAsync(GetOneUserDto getUserDto)
        {
            try
            {
                var user = await _userRepository.GetAsync(x => x.Email == getUserDto.Email);

                if (user != null)
                {
                    Logger.Log($"User with email {getUserDto.Email} was retrieved successfully.", "AuthService.GetUserByEmailAsync()", LogTypes.Info);
                    return user;
                }
                else
                {
                    Logger.Log($"User with email {getUserDto.Email} was not found.", "AuthService.GetUserByEmailAsync()", LogTypes.Info);
                    return null!;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "AuthService.GetUserByEmailAsync()", LogTypes.Error);
                return null!;
            }
        }

        public async Task<UserEntity> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetAsync(x => x.Id == userId);

                if (user != null)
                {
                    Logger.Log($"User with ID {userId} was retrieved successfully.", "AuthService.GetUserByIdAsync()", LogTypes.Info);
                    return user;
                }
                else
                {
                    Logger.Log($"User with ID {userId} was not found.", "AuthService.GetUserByIdAsync()", LogTypes.Info);
                    return null!;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "AuthService.GetUserByIdAsync()", LogTypes.Error);
                return null!;
            }
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();

                Logger.Log("All users were retrieved successfully.", "AuthService.GetAllUsersAsync()", LogTypes.Info);
                return users;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "AuthService.GetAllUsersAsync()", LogTypes.Error);
                return new List<UserEntity>();
            }
        }

        public async Task<bool> RemoveUserAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetAsync(x => x.Email == email);

                if (user != null)
                {
                    var result = await _userRepository.DeleteAsync(x => x.Email == email);

                    if (result)
                    {
                        await _authRepository.DeleteAsync(x => x.UserId == user.Id);

                        Logger.Log($"User with email {email} was successfully removed.", "AuthService.RemoveUserAsync()", LogTypes.Info);
                        return true;
                    }
                    else
                    {
                        Logger.Log($"Failed to remove user with email {email}.", "AuthService.RemoveUserAsync()", LogTypes.Error);
                        return false;
                    }
                }
                else
                {
                    Logger.Log($"User with email {email} was not found.", "AuthService.RemoveUserAsync()", LogTypes.Info);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "AuthService.RemoveUserAsync()", LogTypes.Error);
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            try
            {
                var existingUser = await _userRepository.GetAsync(x => x.Id == updateUserDto.Id);

                if (existingUser != null)
                {
                    existingUser.FirstName = updateUserDto.FirstName;
                    existingUser.LastName = updateUserDto.LastName;
                    existingUser.Email = updateUserDto.Email;

                    var updateResult = await _userRepository.UpdateAsync(x => x.Id == existingUser.Id, existingUser);

                    if (updateResult != null)
                    {
                        Logger.Log($"User with email {existingUser.Email} was successfully updated.", "AuthService.UpdateUserAsync()", LogTypes.Info);
                        return true;
                    }
                    else
                    {
                        Logger.Log($"Failed to update user with email {existingUser.Email}.", "AuthService.UpdateUserAsync()", LogTypes.Error);
                        return false;
                    }
                }
                else
                {
                    Logger.Log($"User with id {updateUserDto.Id} was not found.", "AuthService.UpdateUserAsync()", LogTypes.Info);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "AuthService.UpdateUserAsync()", LogTypes.Error);
                return false;
            }
        }
    }
}
