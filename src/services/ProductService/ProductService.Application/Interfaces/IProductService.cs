using EcommerceShop.Common.Dto;

namespace ProductService.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(Guid productId);
        Task ReserveProductAsync(Guid productId, int quantity);
        Task UpdateProductAsync(ProductDto productDto);
        Task AddProductAsync(ProductDto productDto);
    }
}