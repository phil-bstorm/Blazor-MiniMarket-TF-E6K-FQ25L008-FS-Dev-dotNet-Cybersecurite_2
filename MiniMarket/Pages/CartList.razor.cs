using Microsoft.AspNetCore.Components;
using MiniMarket.Models;
using MiniMarket.Service;

namespace MiniMarket.Pages
{
    /// <summary>
    /// Code-behind pour le composant CartList. 
    /// Ce composant gère l'affichage et la manipulation du panier d'achat.
    /// </summary>
    public partial class CartList
    {
        /// <summary>
        /// Service injecté pour accéder et modifier le panier.
        /// </summary>
        [Inject]
        public CartService CartService { get; set; } = null!;

        /// <summary>
        /// Instance locale du panier, récupérée depuis le service.
        /// </summary>
        public Cart? Cart { get; set; } = null;

        /// <summary>
        /// Méthode appelée à l'initialisation du composant.
        /// Récupère le panier courant via le service.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            Cart = await CartService.GetCartAsync();
        }

        /// <summary>
        /// Supprime un produit du panier en fonction de son ID.
        /// Met ensuite à jour la propriété locale du panier.
        /// </summary>
        /// <param name="productId">Identifiant du produit à retirer</param>
        private void RemoveFromCart(int productId)
        {
            if (Cart != null)
            {
                // Demande au service de retirer le produit
                CartService.RemoveFromCartAsync(productId);

                // Met à jour la copie locale du panier après modification
                Cart = CartService.Cart;
            }
        }

        /// <summary>
        /// Vide complètement le panier.
        /// Met ensuite à jour la propriété locale du panier.
        /// </summary>
        private void ClearCart()
        {
            if (Cart != null)
            {
                // Demande au service de vider le panier
                CartService.ClearCartAsync();

                // Met à jour la copie locale du panier
                Cart = CartService.Cart;
            }
        }

        /// <summary>
        /// Calcule le prix total du panier, en tenant compte des quantités et des promotions éventuelles.
        /// </summary>
        /// <returns>Prix total du panier</returns>
        private double GetTotalPrice()
        {
            double total = 0;

            // Parcours les produits du panier
            foreach (var item in Cart.CartProducts)
            {
                // Prix sans remise pour la quantité
                double itemPrice = item.Product.Price * item.Quantity;

                // Si le produit a une promotion (réduction en pourcentage), on l'applique
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
