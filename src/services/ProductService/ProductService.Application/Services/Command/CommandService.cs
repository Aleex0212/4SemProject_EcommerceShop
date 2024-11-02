using EcommerceShop.Common.Dto;
using EcommerceShop.Common.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using ProductService.Application.Interfaces;
using ProductService.Domain.Models;
using System.Data;

namespace ProductService.Application.Services.Command
{
  public class CommandService : ICommandService
  {
    private readonly IRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ICommandService> _logger;

    public CommandService(IRepository repository, IUnitOfWork unitOfWork, ILogger<ICommandService> logger)
    {
      _repository = repository;
      _unitOfWork = unitOfWork;
      _logger = logger;
    }

    public async Task AddProductAsync(ProductDto productDto)
    {
      try
      {
        _unitOfWork.BeginTransaction(IsolationLevel.Serializable);
        var product = new Product(Guid.NewGuid(), productDto.Name, productDto.Price, productDto.Quantity);
        await _repository.AddProductAsync(product);
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
    public async Task ReserveProductAsync(Guid productId, int quantity)
    {
      try
      {
        _unitOfWork.BeginTransaction(IsolationLevel.Serializable);
        var product = await _repository.GetProductByIdAsync(productId);

        if (product == null) throw new KeyNotFoundException($"Product not found, productId: {productId}. ProductId:{productId}");

        product.Reserve(quantity);
        await _repository.UpdateProductAsync(product);
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
