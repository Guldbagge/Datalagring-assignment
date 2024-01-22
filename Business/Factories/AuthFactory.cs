using Infrastructure.Entities;
using Shared.Utils;

namespace Business.Factories;

public static class AuthFactory
{
    public static AuthEntity Create()
    {
        try
        {
            var authEntity = new AuthEntity();

            return authEntity;

        }
        catch (Exception ex) { Logger.Log(ex.Message, "AuthFactory.Create()", LogTypes.Error); }

        return null!;
    }


    public static AuthEntity Create(int userId, string password)
    {
        try
        {
            var authEntity = new AuthEntity { UserId = userId, Password = PasswordHasher.GenerateSecurePassword(password) };

            return authEntity;

        }
        catch (Exception ex) { Logger.Log(ex.Message, "AuthFactory.Create()", LogTypes.Error); }

        return null!;
    }

    public static AuthEntity Create(int userId, string password, bool isPresistent)
    {
        try
        {
            var authEntity = new AuthEntity { UserId = userId, Password = PasswordHasher.GenerateSecurePassword(password), IsPresistent = isPresistent };

            return authEntity;

        }
        catch (Exception ex) { Logger.Log(ex.Message, "AuthFactory.Create()", LogTypes.Error); }

        return null!;
    }

}
