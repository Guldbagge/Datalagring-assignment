using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Utils;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class Repo<TEntity, TContext> : IRepo<TEntity> where TEntity : class where TContext : DbContext
{
    private readonly TContext _context;

    protected Repo(TContext context)
    {
        _context = context;
    }



    // SQL: INSERT INTO Table VALUES (@Value_1, @Value_2 ...)
    public virtual async Task<TEntity> CreateAsync(TEntity entity, string callingMethod = "")
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex) { Logger.Log(ex.Message, $"Repo.CreateAsync() : {callingMethod}", LogTypes.Error); }

        return null!;
    }



    // SQL: SELECT * FROM Table
    // C#:  GetAsync();
    public virtual async Task<IEnumerable<TEntity>> GetAsync()
    {
        try
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "Repo.GetAsync()", LogTypes.Error); }

        return null!;
    }



    // SQL: SELECT TOP(3) * FROM Table
    // C#:  GetAsync(3);
    public virtual async Task<IEnumerable<TEntity>> GetAsync(int take)
    {
        try
        {
            var entities = await _context.Set<TEntity>().Take(take).ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "Repo.GetAsync()", LogTypes.Error); }

        return null!;
    }



    // SQL: SELECT * FROM Table WHERE Id = @id
    // C#:  GetAsync(x => x.Id == id);
    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "Repo.GetAsync()", LogTypes.Error); }

        return null!;
    }



    // SQL: UPDATE Table SET Column_1 = @Value_1, Column_2 = @Value_2 ... WHERE Id = @id
    // C#:  UpdateAsync(x => x.Id == id, entity);
    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity)
    {
        try
        {
            var updatedEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (updatedEntity != null)
            {
                _context.Entry(updatedEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return updatedEntity;
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "Repo.UpdateAsync()", LogTypes.Error); }

        return null!;
    }



    // SQL: DELETE FROM Table WHERE Id = @id
    // C#:  DeleteAsync(x => x.Id == id);
    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "Repo.DeleteAsync()", LogTypes.Error); }

        return false;
    }



    // SQL: SELECT 1 FROM Table WHERE Id = @id
    // C#:  ExistsAsync(x => x.Id == id);
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var found = await _context.Set<TEntity>().AnyAsync(predicate);
            return found;
        }
        catch (Exception ex) { Logger.Log(ex.Message, "Repo.DeleteAsync()", LogTypes.Error); }

        return false;
    }
}
