using System.ComponentModel.DataAnnotations;

namespace EcommerceShop.Common.Dto
{
    public class ProductDto
    {
        [Required] public Guid Id { get; set; }
        [Required] public string? Name { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] public int Quantity { get; set; }
    }
}