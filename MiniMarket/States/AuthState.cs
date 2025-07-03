using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MiniMarket.States
{
    // il faut installer le package Microsoft.AspNetCore.Components.Authorization
    // Attention à la version si vous êtes en .NET 8
    public class AuthState : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;

        public AuthState(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // Recuperation du token depuis le localStorage
                string token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

                // Lecture du json web token pour voir ce qu'il y a comme information de dans
                if (!string.IsNullOrEmpty(token)) {
                    // il faut installer le NuGet package System.IdentityModel.Tokens.Jwt
                    var jwt = new JwtSecurityToken(token);
                    ClaimsIdentity currentUserIdentity = new ClaimsIdentity(jwt.Claims, "JwtAuth");
                    return new AuthenticationState(new ClaimsPrincipal(currentUserIdentity));
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }
    }
}
