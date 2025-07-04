namespace MiniMarket.Models
{
    // Model representent un produit (reduit) reçu par l'API
    // Ce modèle est utilisé pour les listes de produits et est plus léger que le modèle Product
    public class ProductL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
