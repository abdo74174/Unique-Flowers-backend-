using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FlowerShop.API.DTOs;
using FlowerShop.API.Models;
using FlowerShop.API.Repositories;

namespace FlowerShop.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(string category = null)
        {
            var products = await _repository.GetAllAsync(category);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return null;
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> AddProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var addedProduct = await _repository.AddAsync(product);
            return _mapper.Map<ProductDto>(addedProduct);
        }

        public async Task UpdateProductAsync(int id, CreateProductDto productDto)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct != null)
            {
                _mapper.Map(productDto, existingProduct);
                await _repository.UpdateAsync(existingProduct);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
