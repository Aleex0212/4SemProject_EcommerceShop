namespace ProductService.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }

        public Product(Guid id, string name, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public void Reserve(int quantity)
        {
            if (quantity > Quantity)
                throw new InvalidOperationException("Insufficient quantity.");

            Quantity -= quantity;
        }
    }
}