using EcommerceShop.Common.Dto;

namespace ProductService.Application.Interfaces
{
    /// <summary>
    /// Interface for handling product commands.
    /// </summary>
    public interface IProductCommandService
    {
        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="productDto">The product details to add.</param>
        Task AddProductAsync(ProductDto productDto);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="productDto">The product details to update.</param>
        Task UpdateProductAsync(ProductDto productDto);

        /// <summary>
        /// Deletes an existing product.
        /// </summary>
        /// <param name="productDto">The product to delete.</param>
        Task DeleteProductAsync(ProductDto productDto);

        /// <summary>
        /// Reserves a specified quantity of a product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="quantity">The quantity to reserve.</param>
        Task ReserveProductAsync(Guid productId, int quantity);
    }
}