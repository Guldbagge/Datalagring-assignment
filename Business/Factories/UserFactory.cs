using Infrastructure.Entities;
using Shared.Dtos;
using Shared.Utils;

namespace Business.Factories;

public static class UserFactory
{
    public static (UserEntity, AuthEntity) Create(SignUpDto signUpDto)
    {
        try
        {
            var userEntity = Create(signUpDto.FirstName, signUpDto.LastName, signUpDto.Email, signUpDto.AcceptsUserTerms, signUpDto.AcceptsMarketingTerms);
            var authEntity = AuthFactory.Create(userEntity.Id, signUpDto.Password);

            return (userEntity, authEntity);
        }
        catch (Exception ex) { Logger.Log(ex.Message, "UserFactory.Create()", LogTypes.Error); }

        return (null!, null!);
    }

    public static UserEntity Create()
    {
        try
        {
            var userEntity = new UserEntity();

            return userEntity;
        }
        catch (Exception ex) { Logger.Log(ex.Message, "UserFactory.Create()", LogTypes.Error); }

        return null!;
    }


    public static UserEntity Create(string firstName, string lastName, string email)
    {
        try
        {
            var userEntity = new UserEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            return userEntity;
        }
        catch (Exception ex) { Logger.Log(ex.Message, "UserFactory.Create()", LogTypes.Error); }
        
        return null!;
    }

    public static UserEntity Create(string firstName, string lastName, string email, int roleId)
    {
        try
        {
            var userEntity = new UserEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                RoleId = roleId
            };

            return userEntity;
        }
        catch (Exception ex) { Logger.Log(ex.Message, "UserFactory.Create()", LogTypes.Error); }

        return null!;
    }

    public static UserEntity Create(string firstName, string lastName, string email, bool acceptsUserTerms, bool acceptsMarketingTerms)
    {
        try
        {
            var userEntity = new UserEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                AcceptsUserTerms = acceptsUserTerms,
                AcceptsMarketingTerms = acceptsMarketingTerms
            };

            return userEntity;
        }
        catch (Exception ex) { Logger.Log(ex.Message, "UserFactory.Create()", LogTypes.Error); }

        return null!;
    }

    public static UserEntity Create(string firstName, string lastName, string email, bool acceptsUserTerms, bool acceptsMarketingTerms, int roleId)
    {
        try
        {
            var userEntity = new UserEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                AcceptsUserTerms = acceptsUserTerms,
                AcceptsMarketingTerms = acceptsMarketingTerms,
                RoleId = roleId
            };

            return userEntity;
        }
        catch (Exception ex) { Logger.Log(ex.Message, "UserFactory.Create()", LogTypes.Error); }

        return null!;
    }
}
