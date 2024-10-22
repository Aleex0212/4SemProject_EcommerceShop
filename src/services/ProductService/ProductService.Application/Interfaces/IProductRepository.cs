using ProductService.Domain.Models;

namespace ProductService.Application.Interfaces
{
    /// <summary>
    /// Handles data access for products.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>A list of products.</returns>
        Task<IEnumerable<Product>> GetAllProductsAsync();

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>The product with the specified ID.</returns>
        Task<Product> GetProductByIdAsync(Guid id);

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">The product to add.</param>
        Task AddProductAsync(Product product);

        /// <summary>
        /// 'Soft' deletes a product by setting Property isDeleted=true.
        /// </summary>
        /// <param name="id">Product ID.</param>
        Task DeleteProductAsync(Guid id);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">The updated product data.</param>
        Task UpdateProductAsync(Product product);
    }
}