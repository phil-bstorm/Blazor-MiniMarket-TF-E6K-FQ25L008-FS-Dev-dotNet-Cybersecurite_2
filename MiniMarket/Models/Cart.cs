namespace MiniMarket.Models
{
    public class Cart
    {
        public List<CartProduct> CartProducts { get; set; } = [];
    }

    public class CartProduct
    {
        public required Product Product { get; set; }
        public required int Quantity { get; set; }
    }
}
