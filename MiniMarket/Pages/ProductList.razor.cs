using Microsoft.AspNetCore.Components;
using MiniMarket.Models;
using MiniMarket.Service;

namespace MiniMarket.Pages
{
    public partial class ProductList
    {
        [Inject]
        public ProductService ProductService { get; set; } = null!;
        [Inject]
        public CartService CartService { get; set; } = null!;

        public List<ProductL>? Products { get; set; } = null;
        public Dictionary<int, int> Quantities { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Products = await ProductService.GetProductsAsync();
            if (Products != null)
            {
                Console.WriteLine("foreach");
                foreach (var product in Products)
                {
                    Quantities[product.Id] = 1; // valeur par défaut
                }
            }
        }

        private async Task DeleteProduct(int id)
        {
            if (Products != null)
            {
                await ProductService.DeleteProductAsync(id);
                Products = await ProductService.GetProductsAsync();
            }
        }

        private async Task AddToCart(int productId)
        {
            ProductL? p = Products?.FirstOrDefault(x => x.Id == productId);
            if (p is null)
            {
                return; // Produit non trouvé
            }

            CartProduct cp = new CartProduct
            {
                Product = p,
                Quantity = Quantities.ContainsKey(productId) ? Quantities[productId] : 1,
            };
            await CartService.AddToCartAsync(cp);
        }
    }
}
