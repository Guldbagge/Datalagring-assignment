using Microsoft.Extensions.Logging;
using Shared.Dtos;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Factories;

public static class GetAllDiversDtoFactory
{
    public static GetAllDiversDto Create(string firstName, string lastName, string email)
    {
        try
        {
            var getAllDiversDto = new GetAllDiversDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            return getAllDiversDto;

        }
        catch (Exception ex) { Logger.Log(ex.Message, "GetAllDiversDtoFactory.Create()", LogTypes.Error); }

        return null!;
    }
}
