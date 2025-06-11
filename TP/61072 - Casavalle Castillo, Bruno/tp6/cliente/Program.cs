using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using cliente; // tu espacio de nombres principal
using cliente.Services; // si ProductoService está aquí

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://http://localhost:5184/") });

builder.Services.AddScoped<ProductoService>();

await builder.Build().RunAsync();