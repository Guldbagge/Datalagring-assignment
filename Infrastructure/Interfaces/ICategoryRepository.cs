using Infrastructure.Entities;
using Infrastructure.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public interface ICategoryRepository : IRepo<Category>
{
    Task<Category> FirstOrDefaultAsync(Expression<Func<Category, bool>> predicate);
}
