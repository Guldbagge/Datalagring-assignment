using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class BankSwishDetailsRepository(CustomerContext context) : Repo<BankSwishDetailsEntity, CustomerContext>(context), IBankSwishDetailsRepository
{
}
