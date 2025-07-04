using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MiniMarket.States
{
    // Nécessite le package Microsoft.AspNetCore.Components.Authorization (.NET 8 compatible)
    // AuthenticationStateProvider est une classe abstraite qui gère l'état d'authentification de l'utilisateur dans Blazor.
    public class AuthState : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime; // Pour accéder au localStorage via JS interop

        public int UserId { get; private set; } // Contient l'ID de l'utilisateur extrait du JWT

        public AuthState(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // Récupération du token depuis le localStorage
                string token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

                // Lecture du json web token pour voir ce qu'il y a comme information de dans
                if (!string.IsNullOrEmpty(token))
                {
                    // Nécessite le package System.IdentityModel.Tokens.Jwt
                    var jwt = new JwtSecurityToken(token); // Lecture du token JWT
                    ClaimsIdentity currentUserIdentity = new ClaimsIdentity(jwt.Claims, "JwtAuth");

                    // Extraire l'ID utilisateur
                    var userIdClaim = jwt.Claims.FirstOrDefault(c => c.Type == "id");
                    if (int.TryParse(userIdClaim?.Value, out int userId))
                    {
                        UserId = userId;
                    }

                    // Création du ClaimsPrincipal (permet la gestion des rôles et permissions)
                    return new AuthenticationState(new ClaimsPrincipal(currentUserIdentity));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            // Aucun token valide : retourner un utilisateur anonyme
            return new AuthenticationState(new ClaimsPrincipal());
        }

        /// <summary>
        /// Notifie les composants que l'état d'authentification a changé.
        /// Doit être appelée après modification du token ou déconnexion.
        /// </summary>
        public void NotifyStateChanged()
        {
            // On appelle GetAuthenticationStateAsync pour récupérer l'état d'authentification actuel et mettre à jour l'information de si l'utlisateur est connecté ou non
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
