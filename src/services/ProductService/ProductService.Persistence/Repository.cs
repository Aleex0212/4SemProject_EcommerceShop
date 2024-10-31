using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Models;
using ProductService.Persistence.Context;

namespace ProductService.Persistence
{
  public class Repository : IRepository
  {
    private readonly ProductDbContext _context;

    public Repository(ProductDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
      return await _context.Products
          .Where(p => !p.IsDeleted)
          .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
      var product = await _context.Products.FindAsync(id);
      if (product == null || product.IsDeleted)
        throw new KeyNotFoundException($"Product not found or has been deleted. ProductId:{id}");

      return product;
    }

    public async Task AddProductAsync(Product product)
    {
      try
      {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        throw new Exception($"An error occurred while adding the product. with productId : {product.Id}", ex);
      }
    }

    public async Task DeleteProductAsync(Guid id)
    {
      try
      {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
          product.IsDeleted = true;
          await _context.SaveChangesAsync();
        }
        else
        {
          throw new KeyNotFoundException($"Product with productId: {id} not found.");
        }
      }
      catch (Exception ex)
      {
        throw new Exception($"An error occurred while deleting the product. with productId:{id}", ex);
      }
    }

    public async Task UpdateProductAsync(Product product)
    {
      try
      {
        var existingProduct = await _context.Products.FindAsync(product.Id);
        if (existingProduct == null || existingProduct.IsDeleted)
          throw new KeyNotFoundException($"Product not found or has been deleted. ProductId: {product.Id}");

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        throw new Exception($"An error occurred while updating the product. ProductId:{product.Id} ", ex);
      }
    }
  }
}
