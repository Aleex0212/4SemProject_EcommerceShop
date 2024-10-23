using EcommerceShop.Common.Dto;
using EcommerceShop.Common.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using ProductService.Application.Interfaces;
using ProductService.Domain.Models;
using System.Data;

namespace ProductService.Application.Services.Command
{
    public class ProductCommandService : IProductCommandService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork; 
        private readonly ILogger<IProductCommandService> _logger;

        public ProductCommandService(IProductRepository productRepository, IUnitOfWork unitOfWork, ILogger<IProductCommandService> logger)
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
                _logger.LogError("Error creating product. Exception: {Message}", ex.Message);
            }
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            try
            {
                _unitOfWork.BeginTransaction(IsolationLevel.Serializable);
                var product = await _productRepository.GetProductByIdAsync(productDto.Id);
                if (product == null) throw new KeyNotFoundException($"Product not found. ProductId:{productDto.Id}");
               
                product.Name = productDto.Name;
                product.Quantity = productDto.Quantity;

                await _productRepository.UpdateProductAsync(product);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError("Error updating product. Exception: {Message}", ex.Message);
            }
        }

        public async Task DeleteProductAsync(ProductDto productDto)
        {
            try
            {
                _unitOfWork.BeginTransaction(IsolationLevel.Serializable);
                var product = await _productRepository.GetProductByIdAsync(productDto.Id);

                if (product == null) throw new KeyNotFoundException($"Product not found. ProductId:{productDto.Id}");
                
                await _productRepository.DeleteProductAsync(product.Id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError("Error deleting product. Exception: {Message}", ex.Message);
            }
        }

        public async Task ReserveProductAsync(Guid productId, int quantity)
        {
            try
            {
                _unitOfWork.BeginTransaction(IsolationLevel.Serializable);
                var product = await _productRepository.GetProductByIdAsync(productId);
                
                if (product == null) throw new KeyNotFoundException($"Product not found, productId: {productId}. ProductId:{productId}");
                
                product.Reserve(quantity);
                await _productRepository.UpdateProductAsync(product);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError($"Error reserving product, productId: {productId}. Exception: {ex.Message}");
            }
        }
    }
}
