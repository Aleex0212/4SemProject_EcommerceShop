using EcommerceShop.Common.Dto;

namespace ProductService.Application.Interfaces
{
    /// <summary>
    /// Interface for managing product operations.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of <see cref="ProductDto"/>.</returns>
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <returns>The <see cref="ProductDto"/> corresponding to the specified product ID.</returns>
        Task<ProductDto> GetProductByIdAsync(Guid productId);

        /// <summary>
        /// Reserves a specified quantity of a product.
        /// </summary>
        /// <param name="productId">The ID of the product to reserve.</param>
        /// <param name="quantity">The quantity of the product to reserve.</param>
        Task ReserveProductAsync(Guid productId, int quantity);

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="productDto">The product dto object containing product details.</param>
        /// <returns>A task for asynchronous operation.</returns>
        Task AddProductAsync(ProductDto productDto);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="productDto">The product dto object containing product to add.</param>
        /// <returns>A task for asynchronous operation.</returns>
        Task UpdateProductAsync(ProductDto productDto);

        /// <summary>
        /// 'Soft' deletes a product by setting Property isDeleted=true.
        /// </summary>
        /// <param name="productDto">The product data transfer object containing the product to delete.</param>
        /// <returns>A task for asynchronous operation.</returns>
        Task DeleteProductAsync(ProductDto productDto);
    }
}
