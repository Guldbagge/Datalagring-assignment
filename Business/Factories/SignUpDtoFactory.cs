using Shared.Dtos;
using Shared.Utils;

namespace Business.Factories;

public static class SignUpDtoFactory
{
    public static SignUpDto Create(string firstName, string lastName, string email, string password, bool acceptsUserTerms, bool acceptsMarketingTerms)
    {
        try
        {
            var signUpDto = new SignUpDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                AcceptsUserTerms = acceptsUserTerms,
                AcceptsMarketingTerms = acceptsUserTerms
            };

            return signUpDto;

        }
        catch (Exception ex) { Logger.Log(ex.Message, "SignUpDtoFactory.Create()", LogTypes.Error); }

        return null!;
    }
}
