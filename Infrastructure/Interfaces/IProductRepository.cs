using Infrastructure.Entities;
using Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public interface IProductRepository : IRepo<Product>
{
    Task<IEnumerable<Product>> GetWithCategoryAsync();
    Task<IEnumerable<Product>> GetWithCategoryAsync(int take);
    Task<Product> GetWithCategoryAsync(Expression<Func<Product, bool>> predicate);
}
