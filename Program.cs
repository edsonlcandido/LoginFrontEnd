using LoginFrontEnd;
using LoginFrontEnd.Providers;
using LoginFrontEnd.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthStateProvider>();
builder.Services.AddScoped<CustomHttpClientProvider>();
builder.Services.AddScoped<ClienteService>();


// Adicione a autenticação
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
