//using Infrastructure.Contexts;
//using Infrastructure.Entities;

//namespace Infrastructure.Repositories;


//public class CategoryRepository(ProductCatalogContext context) : Repo<Category, ProductCatalogContext>(context), ICategoryRepository
//{
//    private readonly ProductCatalogContext _context = context;
//}
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : Repo<Category, ProductCatalogContext>, ICategoryRepository
    {
        private readonly ProductCatalogContext _context;

        public CategoryRepository(ProductCatalogContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> FirstOrDefaultAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _context.Categories.FirstOrDefaultAsync(predicate);
        }
    }
}
