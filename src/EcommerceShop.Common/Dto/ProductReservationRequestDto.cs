namespace EcommerceShop.Common.Dto
{
    public class ProductReservationRequestDto
    {
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
