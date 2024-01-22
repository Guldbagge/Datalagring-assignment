using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class RoleRepository(CustomerContext context) : Repo<RoleEntity, CustomerContext>(context), IRoleRepository
{
    private readonly CustomerContext _context = context;
}
