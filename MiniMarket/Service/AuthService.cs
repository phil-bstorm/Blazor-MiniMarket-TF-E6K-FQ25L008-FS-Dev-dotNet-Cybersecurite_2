using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MiniMarket.Models;
using MiniMarket.States;
using System.Net.Http.Json;

namespace MiniMarket.Service
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationStateProvider _authState;

        public AuthService(HttpClient httpClient, 
            IJSRuntime jSRuntime, 
            AuthenticationStateProvider authState)
        {
            _httpClient = httpClient;
            _jsRuntime = jSRuntime;
            _authState = authState;
        }

        public async Task<bool> LoginAsync(LoginForm form)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", form);

            if (response.IsSuccessStatusCode) {
                LoginResponse? content = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (content is not null)
                {
                    Console.WriteLine("Token reçu: " + content.Token);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", content.Token);
                    // équivalent JS de : localStorage.setItem("token", content.Token);

                    // Notifier à l'application Blazor que le status à changer
                    (_authState as AuthState).NotifyStateChanged();

                    return true;
                }
            }


            return false;
        }
    }
}
