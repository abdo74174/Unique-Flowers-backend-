using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlowerShop.API.DTOs;
using FlowerShop.API.Services;

namespace FlowerShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IImageService _imageService;

        public ProductsController(IProductService service, IImageService imageService)
        {
            _service = service;
            _imageService = imageService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] string category = null)
        {
            var products = await _service.GetProductsAsync(category);
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromForm] CreateProductDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (productDto.ImageFile != null)
            {
                var uploadResult = await _imageService.AddImageAsync(productDto.ImageFile);
                if (uploadResult.Error != null) return BadRequest(uploadResult.Error.Message);
                productDto.ImageUrl = uploadResult.SecureUrl.ToString();
            }

            var createdProduct = await _service.AddProductAsync(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] CreateProductDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingProduct = await _service.GetProductByIdAsync(id);
            if (existingProduct == null) return NotFound();

            if (productDto.ImageFile != null)
            {
                var uploadResult = await _imageService.AddImageAsync(productDto.ImageFile);
                if (uploadResult.Error != null) return BadRequest(uploadResult.Error.Message);
                productDto.ImageUrl = uploadResult.SecureUrl.ToString();
            }

            await _service.UpdateProductAsync(id, productDto);
            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existingProduct = await _service.GetProductByIdAsync(id);
            if (existingProduct == null) return NotFound();

            await _service.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
