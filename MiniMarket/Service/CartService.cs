using MiniMarket.Models;

namespace MiniMarket.Service
{
    public class CartService
    {
        public Cart Cart { get; set; } = new Cart();

        public Task<Cart> GetCartAsync()
        {
            return Task.FromResult(Cart);
        }

        public Task AddToCartAsync(CartProduct cartProduct)
        {
            var existingProduct = Cart.CartProducts.FirstOrDefault(cp => cp.Product.Id == cartProduct.Product.Id);
            if (existingProduct != null)
            {
                existingProduct.Quantity += cartProduct.Quantity;
            }
            else
            {
                Cart.CartProducts.Add(cartProduct);
            }
            return Task.CompletedTask;
        }

        public Task RemoveFromCartAsync(int productId)
        {
            var productToRemove = Cart.CartProducts.FirstOrDefault(cp => cp.Product.Id == productId);
            if (productToRemove != null)
            {
                Cart.CartProducts.Remove(productToRemove);
            }
            return Task.CompletedTask;
        }

        public Task ClearCartAsync()
        {
            Cart.CartProducts.Clear();
            return Task.CompletedTask;
        }
    }
}
