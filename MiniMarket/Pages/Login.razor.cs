using Microsoft.AspNetCore.Components;
using MiniMarket.Models;
using MiniMarket.Service;

namespace MiniMarket.Pages
{
    /// <summary>
    /// Code-behind pour le composant Login. 
    /// Gère la logique métier du formulaire de connexion.
    /// </summary>
    public partial class Login
    {
        /// <summary>
        /// Service d'authentification injecté.
        /// Permet de vérifier les identifiants et récupérer un token JWT.
        /// </summary>
        [Inject]
        public AuthService AuthService { get; set; } = null!;

        /// <summary>
        /// Service de navigation Blazor.
        /// Utilisé pour rediriger l'utilisateur vers une autre page après connexion.
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        /// <summary>
        /// Modèle lié au formulaire d'authentification.
        /// Contient les champs Username et Password.
        /// </summary>
        public LoginForm Model { get; set; } = new LoginForm();

        /// <summary>
        /// Méthode appelée à la soumission valide du formulaire.
        /// Tente de se connecter via le AuthService avec les identifiants saisis.
        /// Si la connexion réussit, redirige vers la page d'accueil.
        /// TODO : afficher un message d'erreur en cas d'échec.
        /// </summary>
        public async Task OnSubmit()
        {
            // Appel asynchrone au service d'authentification avec le modèle saisi
            bool success = await AuthService.LoginAsync(Model);

            if (success)
            {
                // Redirection vers la page d'accueil si la connexion réussit
                NavigationManager.NavigateTo("/");
            }

            // TODO : sinon afficher un message d'erreur à l'utilisateur
        }
    }
}
