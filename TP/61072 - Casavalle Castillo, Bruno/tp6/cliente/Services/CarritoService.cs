using System;
using System.Collections.Generic;
using System.Linq;
using servidor.Models;

public class ItemCarrito
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public string ImagenUrl { get; set; } = string.Empty;

    public decimal Subtotal => Precio * Cantidad;

    public ItemCarrito(Producto producto, int cantidad)
    {
        ProductoId = producto.Id;
        Nombre = producto.Nombre;
        Precio = producto.Precio;
        ImagenUrl = producto.ImagenUrl;
        Cantidad = cantidad;
    }

    public ItemCarrito() { }
}

public class CarritoService
{
    public event Action? OnChange;

    private readonly List<ItemCarrito> carrito = new();

    public IReadOnlyList<ItemCarrito> ObtenerItems() => carrito;

    public void AgregarProducto(Producto producto, int cantidad)
    {
        var existente = carrito.FirstOrDefault(i => i.ProductoId == producto.Id);
        if (existente != null)
        {
            existente.Cantidad += cantidad;
        }
        else
        {
            carrito.Add(new ItemCarrito(producto, cantidad));
        }
        NotificarCambio();
    }

    public void EliminarProducto(int productoId)
    {
        var item = carrito.FirstOrDefault(i => i.ProductoId == productoId);
        if (item != null)
        {
            carrito.Remove(item);
            NotificarCambio();
        }
    }

    public void VaciarCarrito()
    {
        carrito.Clear();
        NotificarCambio();
    }

    public decimal CalcularTotal() => carrito.Sum(i => i.Subtotal);

    private void NotificarCambio() => OnChange?.Invoke();
}