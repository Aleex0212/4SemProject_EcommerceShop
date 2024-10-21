using ProductService.Domain.Interfaces;
using ProductService.Domain.Models;

namespace ProductService.Persistence
{
    public class ProductRepository : IProductRepository
    {
        /*private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }*/
        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}