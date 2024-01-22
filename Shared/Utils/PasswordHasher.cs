using System.Security.Cryptography;
using System.Text;

namespace Shared.Utils;

public static class PasswordHasher
{
    public static string GenerateSecurePassword(string password)
    {
        try
        {
            using var hmac = new HMACSHA256();
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
        catch (Exception ex) { Logger.Log(ex.Message, "PasswordHasher.GenerateSecurePassword()", LogTypes.Error); }

        return null!;
    }
}
