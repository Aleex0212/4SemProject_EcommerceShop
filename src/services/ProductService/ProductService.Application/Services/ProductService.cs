using EcommerceShop.Common.Dto;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interfaces;
using ProductService.Domain.Models;

namespace ProductService.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity
            }).ToList();
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity
            };
        }

        public async Task ReserveProductAsync(Guid productId, int quantity)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null)
                throw new InvalidOperationException("Product not found.");

            product.Reserve(quantity);

            await _productRepository.UpdateProductAsync(product);
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var product = await _productRepository.GetProductByIdAsync(productDto.Id);

            if (product == null)
                throw new InvalidOperationException("Product not found.");

            product.Name = productDto.Name;
            product.Quantity = productDto.Quantity;

            await _productRepository.UpdateProductAsync(product);
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            var product = new Product(Guid.NewGuid(), productDto.Name, productDto.Price, productDto.Quantity);
            await _productRepository.AddProductAsync(product);
        }

    }
}
