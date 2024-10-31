namespace OrderService.Domain.Models
{
    public class Order
    {
        public Guid Id { get; private set; }

        public Guid CustomerId { get; private set;}

        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }
        public bool IsCompleted { get; private set; }
    }
}
