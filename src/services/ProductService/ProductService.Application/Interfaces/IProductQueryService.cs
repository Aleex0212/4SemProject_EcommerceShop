using EcommerceShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Application.Interfaces
{
    /// <summary>
    /// Interface for querying product information.
    /// </summary>
    public interface IProductQueryService
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>The task result contains an IEnumerable of ProductDto.</returns>
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <returns>The task result contains a ProductDto for the specified product ID.</returns>
        Task<ProductDto> GetProductByIdAsync(Guid productId);
    }
}