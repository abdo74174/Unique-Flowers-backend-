using System.Collections.Generic;
using System.Threading.Tasks;
using FlowerShop.API.DTOs;

namespace FlowerShop.API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(string category = null);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> AddProductAsync(CreateProductDto productDto);
        Task UpdateProductAsync(int id, CreateProductDto productDto);
        Task DeleteProductAsync(int id);
    }
}
