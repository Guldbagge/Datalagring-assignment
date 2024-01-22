using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class AuthRepository(CustomerContext context) : Repo<AuthEntity, CustomerContext>(context), IAuthRepository
{
    private readonly CustomerContext _context = context;
}