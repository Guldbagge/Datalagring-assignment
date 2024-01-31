using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class UserTypeRepository(CustomerContext context) : Repo<UserTypeEntity, CustomerContext>(context), IUserTypeRepository
{
}
