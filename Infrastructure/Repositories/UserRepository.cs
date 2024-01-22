using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Utils;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;



public class UserRepository(CustomerContext context) : Repo<UserEntity, CustomerContext>(context), IUserRepository
{
    private readonly CustomerContext _context = context;


    public override async Task<IEnumerable<UserEntity>> GetAsync()
    {
        try
        {
            var entities = await _context.Users.Include(i => i.Role).ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "UserRepository.GetAsync()", LogTypes.Error); }

        return null!;
    }

    public override async Task<IEnumerable<UserEntity>> GetAsync(int take)
    {
        try
        {
            var entities = await _context.Users.Include(i => i.Role).Take(take).ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "UserRepository.GetAsync()", LogTypes.Error); }

        return null!;
    }

    public override async Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Users.Include(i => i.Role).FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "UserRepository.GetAsync()", LogTypes.Error); }

        return null!;
    }


    public async Task<List<UserEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Users.Include(i => i.Role).ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex)
        {
            Logger.Log(ex.Message, "UserRepository.GetAllAsync()", LogTypes.Error);
        }

        return new List<UserEntity>();
    }

}
