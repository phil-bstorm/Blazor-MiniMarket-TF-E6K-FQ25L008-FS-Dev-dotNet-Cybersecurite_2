using MiniMarket.Models;
using System.Net.Http.Json;

namespace MiniMarket.Service
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        /// <summary>
        /// Récupère tous les produits depuis l'API
        /// </summary>
        public async Task<List<ProductL>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ProductL>>("api/product");
                return response ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur GetProductsAsync: {ex.Message}");
                return [];
            }
        }

        /// <summary>
        /// Récupère un produit par son identifiant via l'API
        /// </summary>
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Product>($"api/product/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur GetProductByIdAsync({id}): {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Crée un nouveau produit via l'API
        /// </summary>
        public async Task<bool> CreateProductAsync(ProductCreateForm form)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/product", form);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur CreateProductAsync: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Met à jour un produit existant via l'API
        /// </summary>
        public async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/product/{product.Id}", product);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur UpdateProductAsync({product.Id}): {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Supprime un produit par son identifiant via l'API
        /// </summary>
        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/product/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur DeleteProductAsync({id}): {ex.Message}");
                return false;
            }
        }
    }
}
