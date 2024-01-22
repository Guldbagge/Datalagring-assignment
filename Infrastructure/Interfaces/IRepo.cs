using System.Linq.Expressions;

namespace Infrastructure.Interfaces;

public interface IRepo<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity, string callingMethod = "");
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetAsync();
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetAsync(int take);
    Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity);
}