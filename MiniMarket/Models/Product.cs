namespace MiniMarket.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string Description { get; set; }

        public Product(int id, string name, double price, double discount, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Discount = discount;
            Description = description;
        }

        public Product() { }
    }

}
