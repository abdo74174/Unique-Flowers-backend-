using System.Collections.Generic;
using System.Threading.Tasks;
using FlowerShop.API.Models;

namespace FlowerShop.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(string category = null);
        Task<Product> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id); // Soft delete handled in implementation
        Task<bool> ExistsAsync(int id);
    }
}
