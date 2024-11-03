using EcommerceShop.Common.Dto;
using EcommerceShop.Common.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using ProductService.Application.Interfaces;
using ProductService.Domain.Models;
using System.Data;
using EcommerceShop.Common.IntegrationEvents;

namespace ProductService.Application.Services.Command
{
  public class CommandService : ICommandService
  {
    private readonly IRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ICommandService> _logger;
    private readonly IProductMapper _productMapper;

    public CommandService(IRepository repository, IUnitOfWork unitOfWork, ILogger<ICommandService> logger, IProductMapper productMapper)
    {
      _repository = repository;
      _unitOfWork = unitOfWork;
      _logger = logger;
      _productMapper = productMapper;
    }

    public async Task ReserveProductAsync(ReserveProductDto productDtos)
    {
      try
      {
        _unitOfWork.BeginTransaction(IsolationLevel.Serializable);

        foreach (var productDto in productDtos.Products)
        {
          var product = await _repository.GetProductByIdAsync(productDto.ProductDto.Id);

          product.Quantity = Product.Reserve(product.Quantity, productDto.Quantity);

          await _repository.UpdateProductAsync(product);
        }
        _unitOfWork.Commit();
      }
      catch (Exception ex)
      {
        _unitOfWork.Rollback();
        _logger.LogError($"Error reserving product(s) with CorrelationId {productDtos.CorrelationId}: {ex.Message}");
        throw; 
      }
    }




    public async Task AddProductAsync(ProductDto productDto)
    {
      Product? product = null;
      try
      {
        _unitOfWork.BeginTransaction(IsolationLevel.Serializable);
        product = _productMapper.MapToDomainModel(productDto);
        await _repository.AddProductAsync(product);
        _unitOfWork.Commit();
      }
      catch (Exception ex)
      {
        _unitOfWork.Rollback();
        _logger.LogError($"Error creating product with Id: {product!.Id}. Exception: {ex.Message}");
      }
    }

    public async Task UpdateProductAsync(ProductDto productDto)
    {
      try
      {
        _unitOfWork.BeginTransaction(IsolationLevel.Serializable);
        var product = await _repository.GetProductByIdAsync(productDto.Id);
        if (product == null) throw new KeyNotFoundException($"Product not found. ProductId:{productDto.Id}");

        product.Name = productDto.Name;
        product.Quantity = productDto.Quantity;

        await _repository.UpdateProductAsync(product);
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
        var product = await _repository.GetProductByIdAsync(productDto.Id);

        if (product == null) throw new KeyNotFoundException($"Product not found. ProductId:{productDto.Id}");

        await _repository.DeleteProductAsync(product.Id);
        _unitOfWork.Commit();
      }
      catch (Exception ex)
      {
        _unitOfWork.Rollback();
        _logger.LogError("Error deleting product. Exception: {Message}", ex.Message);
      }
    }
  }
}
