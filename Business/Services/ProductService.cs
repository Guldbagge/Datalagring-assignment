
using Business.Factories;
using Business.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Shared.Dtos;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<bool> CheckIfProductExistsAsync(string articleNumber)
        {
            if (await _productRepository.ExistsAsync(x => x.ArticleNumber == articleNumber))
            {
                Logger.Log($"Product with ArticleNumber {articleNumber} already exists.", "ProductService.AddProductAsync()", LogTypes.Info);
                return true;
            }

            return false;
        }


        public async Task<bool> AddProductAsync(AddProductDto addProductDto)
        {
            try
            {
                if (!await CheckIfProductExistsAsync(addProductDto.ArticleNumber))
                {
                    var productEntity = ProductFactory.Create(addProductDto);

                    if (productEntity != null)
                    {
                        var categoryEntity = await GetOrCreateCategoryAsync(addProductDto.CategoryName);

                        productEntity.CategoryId = categoryEntity.Id;

                        Logger.Log($"CategoryId of the product entity: {productEntity.CategoryId}", "ProductService.AddProductAsync()", LogTypes.Info);

                        var result = await CreateProductAsync(productEntity);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ProductService.AddProductAsync()", LogTypes.Error);
            }
            return false;
        }

        public async Task<AddProductDto> GetProductAsync(string articleNumber)
        {
            try
            {
                var productEntity = await _productRepository.GetAsync(x => x.ArticleNumber == articleNumber);
                if (productEntity != null)
                {
                    // Map the Product entity to a DTO
                    var productDto = MapToProductDto(productEntity);
                    return productDto;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ProductService.GetProductAsync()", LogTypes.Error);
            }
            return null;
        }


        public async Task<IEnumerable<AddProductDto>> GetAllProductsAsync()
        {
            try
            {
                var productEntities = await _productRepository.GetAsync();
                if (productEntities != null)
                {
                    // Map the list of Product entities to a list of DTOs
                    var productDtos = productEntities.Select(MapToProductDto);
                    return productDtos;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ProductService.GetAllProductsAsync()", LogTypes.Error);
            }
            return null;
        }

        public async Task<bool> UpdateProductAsync(AddProductDto updatedProductDto)
        {
            try
            {
                var existingProduct = await _productRepository.GetAsync(x => x.ArticleNumber == updatedProductDto.ArticleNumber);

                if (existingProduct != null)
                {
                    // Update properties of the existing product
                    existingProduct.Title = updatedProductDto.Title;
                    existingProduct.Description = updatedProductDto.Description;
                    existingProduct.Price = updatedProductDto.Price;

                    // Add logic to update other properties if needed

                    // Update the product in the repository
                    var result = await _productRepository.UpdateAsync(x => x.ArticleNumber == existingProduct.ArticleNumber, existingProduct);
                    return result != null;
                }

                Logger.Log($"Product with ArticleNumber {updatedProductDto.ArticleNumber} not found.", "ProductService.UpdateProductAsync()", LogTypes.Info);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ProductService.UpdateProductAsync()", LogTypes.Error);
                return false;
            }
        }


        public async Task<bool> DeleteProductAsync(string articleNumber)
        {
            try
            {
                var result = await _productRepository.DeleteAsync(x => x.ArticleNumber == articleNumber);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ProductService.DeleteProductAsync()", LogTypes.Error);
                return false;
            }
        }


        private async Task<Category> GetOrCreateCategoryAsync(string categoryName)
        {
            var categoryEntity = await _categoryRepository.GetAsync(x => x.CategoryName == categoryName);

            if (categoryEntity == null)
            {
                categoryEntity = new Category { CategoryName = categoryName };
                categoryEntity = await _categoryRepository.CreateAsync(categoryEntity, nameof(_categoryRepository));

                Logger.Log($"Category '{categoryName}' created.", "ProductService.GetOrCreateCategoryAsync()", LogTypes.Info);
            }

            return categoryEntity;
        }

        private AddProductDto MapToProductDto(Product productEntity)
        {
            if (productEntity != null)
            {
                return new AddProductDto
                {
                    ArticleNumber = productEntity.ArticleNumber,
                    Title = productEntity.Title,
                    Description = productEntity.Description,
                    Price = productEntity.Price,
                    CategoryName = productEntity.Category?.CategoryName ?? string.Empty
                    // Add other properties as needed
                };
            }
            return null;
        }


        private async Task<bool> CreateProductAsync(Product productEntity)
        {
            try
            {
                Logger.Log($"_productRepository is {_productRepository}", "ProductService.CreateProductAsync()", LogTypes.Info);

                productEntity = await _productRepository.CreateAsync(productEntity, nameof(_productRepository));

                if (productEntity != null)
                {
                    Logger.Log("A product was created successfully.", "ProductService.CreateProductAsync()", LogTypes.Info);
                    return true;
                }
                else
                {
                    Logger.Log("ProductEntity is null after creation.", "ProductService.CreateProductAsync()", LogTypes.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ProductService.CreateProductAsync()", LogTypes.Error);
            }

            return false;
        }

        public async Task<Product> GetProductAsync(GetOneProductDto getOneProductDto)
        {
            try
            {
                // Implement the logic to retrieve the product using the DTO.
                var productEntity = await _productRepository.GetAsync(x => x.ArticleNumber == getOneProductDto.ArticleNumber);

                return productEntity;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ProductService.GetProductAsync(GetOneProductDto)", LogTypes.Error);
                return null;
            }
        }

    }
}



//using Business.Factories;
//using Business.Interfaces;
//using Infrastructure.Entities;
//using Infrastructure.Repositories;
//using Shared.Dtos;
//using Shared.Utils;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Business.Services
//{
//    public class ProductService : IProductService
//    {
//        private readonly IProductRepository _productRepository;
//        private readonly ICategoryRepository _categoryRepository;

//        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
//        {
//            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
//            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
//        }

//        public async Task<bool> AddProductAsync(AddProductDto addProductDto)
//        {
//            try
//            {
//                if (!await CheckIfProductExistsAsync(addProductDto.ArticleNumber))
//                {
//                    var productEntity = ProductFactory.Create(addProductDto);

//                    if (productEntity != null)
//                    {
//                        var categoryEntity = await _categoryRepository.GetAsync(x => x.CategoryName == addProductDto.CategoryName);

//                        if (categoryEntity == null)
//                        {
//                            categoryEntity = new Category { CategoryName = addProductDto.CategoryName };
//                            categoryEntity = await _categoryRepository.CreateAsync(categoryEntity, nameof(_categoryRepository));

//                            Logger.Log($"Category '{addProductDto.CategoryName}' created.", "ProductService.AddProductAsync()", LogTypes.Info);
//                        }

//                        productEntity.CategoryId = categoryEntity.Id;

//                        Logger.Log($"CategoryId of the product entity: {productEntity.CategoryId}", "ProductService.AddProductAsync()", LogTypes.Info);

//                        var result = await CreateProductAsync(productEntity);
//                        return result;
//                    }
//                }
//            }
//            catch (Exception ex) { Logger.Log(ex.Message, "ProductService.AddProductAsync()", LogTypes.Error); }
//            return false;
//        }

//        public async Task<bool> CheckIfProductExistsAsync(string articleNumber)
//        {
//            if (await _productRepository.ExistsAsync(x => x.ArticleNumber == articleNumber))
//            {
//                Logger.Log($"Product with ArticleNumber {articleNumber} already exists.", "ProductService.AddProductAsync()", LogTypes.Info);
//                return true;
//            }

//            return false;
//        }

//        public async Task<bool> CreateProductAsync(Product productEntity)
//        {
//            try
//            {
//                Logger.Log($"_productRepository is {_productRepository}", "ProductService.CreateProductAsync()", LogTypes.Info);

//                productEntity = await _productRepository.CreateAsync(productEntity, nameof(_productRepository));

//                if (productEntity != null)
//                {
//                    Logger.Log("A product was created successfully.", "ProductService.CreateProductAsync()", LogTypes.Info);
//                    return true;
//                }
//                else
//                {
//                    Logger.Log("ProductEntity is null after creation.", "ProductService.CreateProductAsync()", LogTypes.Error);
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.Log(ex.Message, "ProductService.CreateProductAsync()", LogTypes.Error);
//            }

//            return false;
//        }
//    }
//}
