using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MiniMarket;
using MiniMarket.Middlewares;
using MiniMarket.Service;
using MiniMarket.States;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Ancien HttpClient qui est remplacé par les lignes suivantes "AddHttpClient"
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7104/") });


builder.Services.AddScoped<TokenInterceptor>();
// nécessite d'installer Microsoft.Extensions.Http
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7104/");
}).AddHttpMessageHandler<TokenInterceptor>();

// Mise à disposition des services utilisés dans l'application
// (Les services sont des classes qui contiennent la logique métier de l'application et qui communique avec l'API)
builder.Services.AddScoped<ProductService>(); // Manipulation des produits
builder.Services.AddScoped<CartService>(); // Gestion du panier
builder.Services.AddScoped<AuthService>(); // Authentification de l'utilisateurs

// Authentification
builder.Services.AddAuthorizationCore(); // active le système d'autorisation, ce qui permet de gérer les rôles et les permissions
builder.Services.AddScoped<AuthenticationStateProvider, AuthState>(); // Gestion de l'état d'authentification de l'utilisateur

await builder.Build().RunAsync();
