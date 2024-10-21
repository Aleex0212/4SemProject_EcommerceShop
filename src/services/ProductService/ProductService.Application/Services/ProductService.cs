using EcommerceShop.Common.Dto;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interfaces;
using ProductService.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Extensions.Logging;
using EcommerceShop.Common.UnitOfWork.Interfaces;

namespace ProductService.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork<ProductDbContext> _unitOfWork;
        private readonly ILogger<IProductService> _logger;

        public ProductService(IProductRepository productRepository, IUnitOfWork<ProductDbContext> unitOfWork, ILogger<IProductService> logger)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            try
            {
                _unitOfWork.BeginTransaction(IsolationLevel.Serializable);

                var product = new Product(Guid.NewGuid(), productDto.Name, productDto.Price, productDto.Quantity);
                await _productRepository.AddProductAsync(product);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError("Error creating product with request. Exception: {Message}", ex.Message);
                throw new ValidationException("Validation failed on product");
            }
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            try
            {
                _unitOfWork.BeginTransaction(IsolationLevel.Serializable);

                var product = await _productRepository.GetProductByIdAsync(productDto.Id);
                if (product == null)
                    throw new KeyNotFoundException("Product not found.");

                product.Name = productDto.Name;
                product.Quantity = productDto.Quantity;

                await _productRepository.UpdateProductAsync(product);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError("Error updating product with request. Exception: {Message}", ex.Message);
                throw new DBConcurrencyException();
            }
        }

        public async Task DeleteProductAsync(ProductDto productDto)
        {
            try
            {
                _unitOfWork.BeginTransaction(IsolationLevel.Serializable);

                var product = await _productRepository.GetProductByIdAsync(productDto.Id);
                if (product == null)
                    throw new KeyNotFoundException("Product not found.");

                await _productRepository.DeleteProductAsync(product);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError("Error deleting product with request. Exception: {Message}", ex.Message);
            }
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
    }
}
