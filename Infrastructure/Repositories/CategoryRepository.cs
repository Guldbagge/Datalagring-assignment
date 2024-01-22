using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;


public class CategoryRepository(ProductCatalogContext context) : Repo<Category, ProductCatalogContext>(context), ICategoryRepository
{
    private readonly ProductCatalogContext _context = context;
}
