using System.ComponentModel.DataAnnotations;

namespace MiniMarket.Models
{
    // Formulaire utiliser pour créer un produit (ce formulaire est conforme au données attendue par l'API)
    public class ProductCreateForm
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; } = 0;

        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public double Discount { get; set; } = 0;

        [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } = "";
    }
}
