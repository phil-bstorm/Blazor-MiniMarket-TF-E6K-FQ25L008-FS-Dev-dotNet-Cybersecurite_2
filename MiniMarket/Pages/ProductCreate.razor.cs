using Microsoft.AspNetCore.Components;
using MiniMarket.Models;
using MiniMarket.Service;

namespace MiniMarket.Pages
{
    public partial class ProductCreate
    {
        [Inject]
        public ProductService ProductService { get; set; } = null!; // Service pour gérer les produits

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!; // Pour rediriger après création

        public ProductCreateForm Model { get; set; } = new ProductCreateForm(); // Modèle du formulaire

        public async Task OnSubmit()
        {
            await ProductService.CreateProductAsync(Model); // Enregistre le nouveau produit

            NavigationManager.NavigateTo("/product"); // Redirige vers la liste des produits
        }
    }
}
