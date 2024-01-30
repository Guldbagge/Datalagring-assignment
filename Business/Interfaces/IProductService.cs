using Infrastructure.Entities;
using Shared.Dtos;

namespace Business.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(AddProductDto addProductDto);
        Task<bool> CheckIfProductExistsAsync(string articleNumber);
        Task<bool> DeleteProductAsync(string articleNumber);
        Task<IEnumerable<AddProductDto>> GetAllProductsAsync();
        Task<AddProductDto> GetProductAsync(string articleNumber);
        Task<bool> UpdateProductAsync(AddProductDto updatedProductDto);
        Task<Product> GetProductAsync(GetOneProductDto getOneProductDto);

    }
}