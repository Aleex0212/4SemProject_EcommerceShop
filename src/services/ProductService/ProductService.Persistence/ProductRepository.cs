﻿using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Interfaces;
using ProductService.Domain.Models;

namespace ProductService.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext.ProductDbContext _context;

        public ProductRepository(ProductDbContext.ProductDbContext context)
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
                throw new KeyNotFoundException("Product not found or has been deleted.");

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
                throw new Exception("An error occurred while adding the product.", ex);
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
                    throw new KeyNotFoundException("Product not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the product.", ex);
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                var existingProduct = await _context.Products.FindAsync(product.Id);
                if (existingProduct == null || existingProduct.IsDeleted)
                    throw new KeyNotFoundException("Product not found or has been deleted.");

                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the product.", ex);
            }
        }
    }
}
