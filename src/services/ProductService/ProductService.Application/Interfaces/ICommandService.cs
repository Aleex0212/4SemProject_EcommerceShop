using EcommerceShop.Common.Dto;

namespace ProductService.Application.Interfaces
{
  /// <summary>
  /// Interface for handling product commands.
  /// </summary>
  public interface ICommandService
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
    /// Reserves a product.
    /// </summary>
    /// <param name="productDto">The product to reserve.</param>
    /// /// <param name="quantity">quantity to reserve</param>
    Task ReserveProductAsync(Guid productId, int quantity);
  }
}