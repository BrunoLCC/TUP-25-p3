using Microsoft.AspNetCore.Components.Web;
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:7221/") });

builder.Services.AddScoped<ProductoService>();