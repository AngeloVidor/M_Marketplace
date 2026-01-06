namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public Guid VendorProfileId { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public ProductCategory Category { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Product() { }

        public Product(
            Guid vendorProfileId,
            string name,
            string description,
            decimal price,
            int stock,
            ProductCategory category
        )
        {
            Id = Guid.NewGuid();
            VendorProfileId = vendorProfileId;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Category = category;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name, string description, decimal price, int stock, ProductCategory category)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Category = category;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive");
            if (quantity > Stock) throw new InvalidOperationException("Not enough stock");
            Stock -= quantity;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive");
            Stock += quantity;
        }

    }
}
