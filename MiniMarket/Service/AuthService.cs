using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MiniMarket.Models;
using MiniMarket.States;
using System.Net.Http.Json;

namespace MiniMarket.Service
{
    public class AuthService
    {
        private readonly HttpClient _httpClient; // client HTTP pour appeler l'API
        private readonly IJSRuntime _jsRuntime; // pour accéder au localStorage
        private readonly AuthenticationStateProvider _authState; // pour notifier un changement d'état

        public AuthService(IHttpClientFactory httpClientFactory,
            IJSRuntime jSRuntime,
            AuthenticationStateProvider authState)
        {
            _httpClient = httpClientFactory.CreateClient("API"); // utilise le client nommé "API"
            _jsRuntime = jSRuntime;
            _authState = authState;
        }

        public async Task<bool> LoginAsync(LoginForm form)
        {
            // Envoie des identifiants à l'API
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", form);

            if (response.IsSuccessStatusCode)
            {
                // Lit la réponse JSON
                LoginResponse? content = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (content is not null)
                {
                    Console.WriteLine("Token reçu: " + content.Token);

                    // Stocke le token dans le localStorage
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", content.Token);

                    // Notifie Blazor que l'état d'authentification a changé
                    (_authState as AuthState).NotifyStateChanged();

                    return true;
                }
            }

            return false; // échec du login
        }
    }
}
