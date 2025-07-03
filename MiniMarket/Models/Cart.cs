namespace MiniMarket.Models
{
    public class Cart
    {
        public List<CartProduct> CartProducts { get; set; } = [];
    }

    public class CartProduct
    {
        public required ProductL Product { get; set; }
        public required int Quantity { get; set; }
    }
}
