using Microsoft.AspNetCore.Components;
using MiniMarket.Models;
using MiniMarket.Service;

namespace MiniMarket.Pages
{
    public partial class ProductList
    {
        [Inject]
        public ProductService ProductService { get; set; } = null!; // Service pour gérer les produits

        [Inject]
        public CartService CartService { get; set; } = null!; // Service pour gérer le panier

        public List<ProductL>? Products { get; set; } = null; // Liste des produits à afficher
        public Dictionary<int, int> Quantities { get; set; } = new(); // Quantité choisie pour chaque produit

        protected override async Task OnInitializedAsync()
        {
            Products = await ProductService.GetProductsAsync(); // Récupère la liste des produits
            if (Products != null)
            {
                foreach (var product in Products)
                {
                    Quantities[product.Id] = 1; // Quantité par défaut à 1
                }
            }
        }

        private async Task DeleteProduct(int id)
        {
            if (Products != null)
            {
                await ProductService.DeleteProductAsync(id); // Supprime le produit
                Products = await ProductService.GetProductsAsync(); // Recharge la liste après suppression
            }
        }

        private async Task AddToCart(int productId)
        {
            ProductL? p = Products?.FirstOrDefault(x => x.Id == productId);
            if (p is null)
            {
                return; // Ne rien faire si le produit est introuvable
            }

            CartProduct cp = new CartProduct
            {
                Product = p,
                Quantity = Quantities.ContainsKey(productId) ? Quantities[productId] : 1, // Récupère la quantité choisie
            };
            await CartService.AddToCartAsync(cp); // Ajoute l'article au panier
        }
    }
}
