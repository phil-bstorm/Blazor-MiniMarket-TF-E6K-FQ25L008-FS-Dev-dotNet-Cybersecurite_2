using Microsoft.JSInterop;
using MiniMarket.Models;
using System.Net.Http.Json;

namespace MiniMarket.Service
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public AuthService(HttpClient httpClient, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jSRuntime;
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

                    return true;
                }
            }


            return false;
        }
    }
}
