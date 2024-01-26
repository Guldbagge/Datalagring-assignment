using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shared.Utils;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : Repo<Product, ProductCatalogContext>, IProductRepository
    {
        private readonly ProductCatalogContext _context;

        public ProductRepository(ProductCatalogContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

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

        public async Task<Product> CreateAsync(Product entity, string source)
        {
            try
            {
                Logger.Log($"Before SaveChangesAsync - ArticleNumber={entity.ArticleNumber}, Title={entity.Title}, Description={entity.Description}, Price={entity.Price}, CategoryId={entity.CategoryId}", source, LogTypes.Info);

                _context.Set<Product>().Add(entity);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException dbEx)
                {
                    Logger.Log($"Database update error: {dbEx.Message}", source, LogTypes.Error);
                }
                catch (Exception ex)
                {
                    Logger.Log($"Error during SaveChangesAsync: {ex.Message}", source, LogTypes.Error);
                }

                Logger.Log($"After SaveChangesAsync - ArticleNumber={entity.ArticleNumber}, Title={entity.Title}, Description={entity.Description}, Price={entity.Price}, CategoryId={entity.CategoryId}", source, LogTypes.Info);

                return entity;
            }
            catch (Exception ex)
            {
                Logger.Log($"Error during product creation: {ex.Message}", source, LogTypes.Error);
            }

            return null;
        }








        //public async Task<Product> CreateAsync(Product entity, string source)
        //{
        //    try
        //    {
        //        // Logga informationen innan produkten sparas
        //        Logger.Log($"Before SaveChangesAsync - ArticleNumber={entity.ArticleNumber}, Title={entity.Title}, Description={entity.Description}, Price={entity.Price}, CategoryId={entity.CategoryId}", source, LogTypes.Info);

        //        _context.Set<Product>().Add(entity);
        //        await _context.SaveChangesAsync();

        //        // Logga informationen efter att produkten har sparats
        //        Logger.Log($"After SaveChangesAsync - ArticleNumber={entity.ArticleNumber}, Title={entity.Title}, Description={entity.Description}, Price={entity.Price}, CategoryId={entity.CategoryId}", source, LogTypes.Info);

        //        return entity;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message, source, LogTypes.Error);
        //        return null;
        //    }
        //}
    }
}




//using Infrastructure.Contexts;
//using Infrastructure.Entities;
//using Microsoft.EntityFrameworkCore;
//using Shared.Utils;
//using System.Linq.Expressions;

//namespace Infrastructure.Repositories;

//public class ProductRepository(ProductCatalogContext context) : Repo<Product, ProductCatalogContext>(context), IProductRepository
//{
//    private readonly ProductCatalogContext _context = context;

//    public async Task<IEnumerable<Product>> GetWithCategoryAsync()
//    {
//        try
//        {
//            var entities = await _context.Products.Include(i => i.Category).ToListAsync();
//            if (entities.Count != 0)
//            {
//                return entities;
//            }
//        }
//        catch (Exception ex) { Logger.Log(ex.Message, "ProductRepository.GetWithCategoryAsync()", LogTypes.Error); }

//        return null!;
//    }

//    public async Task<IEnumerable<Product>> GetWithCategoryAsync(int take)
//    {
//        try
//        {
//            var entities = await _context.Products.Include(i => i.Category).Take(take).ToListAsync();
//            if (entities.Count != 0)
//            {
//                return entities;
//            }
//        }
//        catch (Exception ex) { Logger.Log(ex.Message, "ProductRepository.GetWithCategoryAsync()", LogTypes.Error); }

//        return null!;
//    }

//    public async Task<Product> GetWithCategoryAsync(Expression<Func<Product, bool>> predicate)
//    {
//        try
//        {
//            var entity = await _context.Products.Include(i => i.Category).FirstOrDefaultAsync(predicate);
//            if (entity != null)
//            {
//                return entity;
//            }
//        }
//        catch (Exception ex) { Logger.Log(ex.Message, "ProductRepository.GetWithCategoryAsync()", LogTypes.Error); }

//        return null!;
//    }



//}
