using Shared.Dtos;

namespace Business.Interfaces;
public interface IProductService
{
    Task<bool> AddProductAsync(AddProductDto addProductDto);
}