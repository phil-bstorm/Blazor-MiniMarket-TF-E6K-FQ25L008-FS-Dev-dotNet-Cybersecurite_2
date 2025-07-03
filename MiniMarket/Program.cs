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

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7104/") });

builder.Services.AddScoped<TokenInterceptor>();
builder.Services.AddHttpClient("API", client => {
    client.BaseAddress = new Uri("https://localhost:7104/");
}).AddHttpMessageHandler<TokenInterceptor>();


builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<AuthService>();

// Authentification
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthState>();

await builder.Build().RunAsync();
