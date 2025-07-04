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

// Ancien HttpClient qui est remplac� par les lignes suivantes "AddHttpClient"
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7104/") });


builder.Services.AddScoped<TokenInterceptor>();
// n�cessite d'installer Microsoft.Extensions.Http
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7104/");
}).AddHttpMessageHandler<TokenInterceptor>();

// Mise � disposition des services utilis�s dans l'application
// (Les services sont des classes qui contiennent la logique m�tier de l'application et qui communique avec l'API)
builder.Services.AddScoped<ProductService>(); // Manipulation des produits
builder.Services.AddScoped<CartService>(); // Gestion du panier
builder.Services.AddScoped<AuthService>(); // Authentification de l'utilisateurs

// Authentification
builder.Services.AddAuthorizationCore(); // active le syst�me d'autorisation, ce qui permet de g�rer les r�les et les permissions
builder.Services.AddScoped<AuthenticationStateProvider, AuthState>(); // Gestion de l'�tat d'authentification de l'utilisateur

await builder.Build().RunAsync();
