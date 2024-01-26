using Business.Factories;
using Business.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Shared.Dtos;
using Shared.Utils;
using System;
using System.Collections.Generic;
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

        public async Task<bool> AddProductAsync(AddProductDto addProductDto)
        {
            try
            {
                if (!await CheckIfProductExistsAsync(addProductDto.ArticleNumber))
                {
                    var productEntity = ProductFactory.Create(addProductDto);

                    if (productEntity != null)
                    {
                        var categoryEntity = await _categoryRepository.GetAsync(x => x.CategoryName == addProductDto.CategoryName);

                        if (categoryEntity == null)
                        {
                            categoryEntity = new Category { CategoryName = addProductDto.CategoryName };
                            categoryEntity = await _categoryRepository.CreateAsync(categoryEntity, nameof(_categoryRepository));

                            Logger.Log($"Category '{addProductDto.CategoryName}' created.", "ProductService.AddProductAsync()", LogTypes.Info);
                        }

                        productEntity.CategoryId = categoryEntity.Id;

                        Logger.Log($"CategoryId of the product entity: {productEntity.CategoryId}", "ProductService.AddProductAsync()", LogTypes.Info);

                        var result = await CreateProductAsync(productEntity);
                        return result;
                    }
                }
            }
            catch (Exception ex) { Logger.Log(ex.Message, "ProductService.AddProductAsync()", LogTypes.Error); }
            return false;
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

        public async Task<bool> CreateProductAsync(Product productEntity)
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
    }
}
