using System.ComponentModel.DataAnnotations;

namespace MiniMarket.Models
{
    public class LoginForm
    {
        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}
