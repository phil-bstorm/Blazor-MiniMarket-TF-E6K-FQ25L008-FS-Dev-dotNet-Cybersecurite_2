using Microsoft.AspNetCore.Components;
using MiniMarket.Models;
using MiniMarket.Service;

namespace MiniMarket.Pages
{
    public partial class CartList
    {
        [Inject]
        public CartService CartService { get; set; } = null!;

        public Cart? Cart { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            Cart = await CartService.GetCartAsync();
        }

        private void RemoveFromCart(int productId)
        {
            if (Cart != null)
            {
                CartService.RemoveFromCartAsync(productId);
                Cart = CartService.Cart; // Mettre à jour le cart après la suppression
            }
        }

        private void ClearCart()
        {
            if (Cart != null)
            {
                CartService.ClearCartAsync();
                Cart = CartService.Cart; // Mettre à jour le cart après le vidage
            }
        }

        private double GetTotalPrice()
        {
            double total = 0;
            foreach (var item in Cart.CartProducts)
            {
                double itemPrice= item.Product.Price * item.Quantity;

                // Si le produit a une promotion, appliquer la réduction
                if (item.Product.Discount >= 0)
                {
                    itemPrice -= itemPrice * (item.Product.Discount / 100);
                }

                total += itemPrice;
            }
            return total;
        }
    }
}
