using Microsoft.EntityFrameworkCore;
using servidor.Data;
using servidor.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TiendaDbContext>(options =>
    options.UseSqlite("Data Source=C:\\Users\\Bruno Casavalle\\Desktop\\TUP-25-p3\\TP\\61072 - Casavalle Castillo, Bruno\\tp6\\servidor\\tienda.db"));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClientApp", policy =>
    {
        policy.WithOrigins("http://localhost:5177", "https://localhost:7221")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TiendaDbContext>();
    db.Database.EnsureCreated();


    if (!db.Productos.Any())
    {
        db.Productos.AddRange(
            new Producto { Nombre = "Celular Samsung A54", Descripcion = "Pantalla AMOLED", Precio = 200000, Stock = 12, },
            new Producto { Nombre = "Notebook HP", Descripcion = "Intel i5, 8GB RAM", Precio = 480000, Stock = 7, },
            new Producto { Nombre = "Mouse Logitech", Descripcion = "Mouse inalámbrico", Precio = 9500, Stock = 25, },
            new Producto { Nombre = "Teclado Redragon", Descripcion = "Teclado mecánico RGB", Precio = 18000, Stock = 10, },
            new Producto { Nombre = "Auriculares Sony", Descripcion = "Cancelación de ruido", Precio = 27000, Stock = 15, },
            new Producto { Nombre = "Monitor LG 24\"", Descripcion = "Full HD", Precio = 125000, Stock = 9, },
            new Producto { Nombre = "Impresora Epson", Descripcion = "Multifunción", Precio = 85000, Stock = 6, },
            new Producto { Nombre = "Parlantes Bluetooth", Descripcion = "Potentes y portátiles", Precio = 32000, Stock = 18, },
            new Producto { Nombre = "Webcam Logitech", Descripcion = "HD 720p", Precio = 14500, Stock = 20, },
            new Producto { Nombre = "Cargador portátil", Descripcion = "10.000 mAh", Precio = 11000, Stock = 30, }
        );

        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowClientApp");


app.MapGet("/", () => "Servidor API está en funcionamiento");

app.MapGet("/api/datos", () => new { Mensaje = "Datos desde el servidor", Fecha = DateTime.Now });


app.MapGet("/api/productos", async (TiendaDbContext db, string? buscar) =>
{
    var productos = db.Productos.AsQueryable();

    if (!string.IsNullOrWhiteSpace(buscar))
        productos = productos.Where(p => p.Nombre.Contains(buscar));

    return await productos.ToListAsync();
});

app.Run();