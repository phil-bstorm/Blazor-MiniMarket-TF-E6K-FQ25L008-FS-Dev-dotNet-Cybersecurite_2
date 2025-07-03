using Microsoft.JSInterop;

namespace MiniMarket.Middlewares
{
    public class TokenInterceptor: DelegatingHandler
    {
        private readonly IJSRuntime _jsRuntime;

        public TokenInterceptor(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken
            )
        {
            // récupere le token
            string token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
            if (!string.IsNullOrEmpty(token)) {
                // si on a un token, on l'ajoute à la requête
                request.Headers.Add("Authorization", "Bearer " + token); 
            }

            // la requête continue
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
