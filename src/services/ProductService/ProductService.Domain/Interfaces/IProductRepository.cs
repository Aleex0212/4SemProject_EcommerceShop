using ProductService.Domain.Models;

namespace ProductService.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
        Task UpdateProductAsync(Product product);
    }
}