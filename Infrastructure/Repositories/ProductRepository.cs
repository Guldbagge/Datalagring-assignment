using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Utils;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProductRepository(ProductCatalogContext context) : Repo<Product, ProductCatalogContext>(context), IProductRepository
{
    private readonly ProductCatalogContext _context = context;

    public async Task<IEnumerable<Product>> GetWithCategoryAsync()
    {
        try
        {
            var entities = await _context.Products.Include(i => i.Category).ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "ProductRepository.GetWithCategoryAsync()", LogTypes.Error); }

        return null!;
    }

    public async Task<IEnumerable<Product>> GetWithCategoryAsync(int take)
    {
        try
        {
            var entities = await _context.Products.Include(i => i.Category).Take(take).ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "ProductRepository.GetWithCategoryAsync()", LogTypes.Error); }

        return null!;
    }

    public async Task<Product> GetWithCategoryAsync(Expression<Func<Product, bool>> predicate)
    {
        try
        {
            var entity = await _context.Products.Include(i => i.Category).FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { Logger.Log(ex.Message, "ProductRepository.GetWithCategoryAsync()", LogTypes.Error); }

        return null!;
    }
}
