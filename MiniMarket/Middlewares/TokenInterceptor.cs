using Microsoft.JSInterop;

namespace MiniMarket.Middlewares
{
    /// <summary>
    /// Middleware HTTP personnalisé pour Blazor WebAssembly.
    /// Cette classe hérite de DelegatingHandler et intercepte toutes les requêtes HTTP sortantes
    /// pour y ajouter automatiquement le token JWT si l'utilisateur est authentifié.
    /// Cela permet de sécuriser les appels API en transmettant le token dans l'en-tête Authorization.
    /// </summary>
    public class TokenInterceptor : DelegatingHandler
    {
        private readonly IJSRuntime _jsRuntime; // Service JavaScript interop pour accéder au localStorage côté navigateur

        /// <summary>
        /// Constructeur avec injection du service IJSRuntime.
        /// </summary>
        public TokenInterceptor(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Méthode qui intercepte chaque requête HTTP sortante.
        /// Si un token est présent dans le localStorage, il est ajouté dans l'en-tête Authorization de la requête.
        /// </summary>
        /// <param name="request">La requête HTTP en cours</param>
        /// <param name="cancellationToken">Token d'annulation</param>
        /// <returns>La réponse HTTP</returns>
        protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken
            )
        {
            // Récupère le token JWT depuis le localStorage via JavaScript interop
            string token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            // Si le token est non vide, on l'ajoute dans les en-têtes HTTP
            if (!string.IsNullOrEmpty(token))
            {
                // L'en-tête Authorization est ajouté sous la forme : Bearer <token>
                request.Headers.Add("Authorization", "Bearer " + token);
            }

            // Appelle la méthode de base pour poursuivre la chaîne d'exécution des handlers
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
