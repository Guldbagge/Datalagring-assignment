using Shared.Dtos;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Factories;

public class UpdateUserDtoFactory
{
    public static UpdateUserDto Create(int userId, string firstName, string lastName, string email, string password)
    {
        try
        {
            var updateUserDto = new UpdateUserDto
            {
                Id = userId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
            };

            return updateUserDto;

        }
        catch (Exception ex)
        {
            Logger.Log(ex.Message, "UpdateUserDtoFactory.Create()", LogTypes.Error);
            throw;
        }
    }
}

