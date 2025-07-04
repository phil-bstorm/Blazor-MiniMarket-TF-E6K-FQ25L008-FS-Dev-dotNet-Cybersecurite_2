using System.ComponentModel.DataAnnotations;

namespace MiniMarket.Models
{
    // Model utiliser lors de l'API dans le AuthService pour la connexion
    public class LoginForm
    {
        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}
