using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliente.Shared;

public class CarritoService
{
    public event Action? OnChange;

    private readonly List<ItemCarrito> carrito = new();

    public IReadOnlyList<ItemCarrito> ObtenerItems() => carrito;

    public void AgregarProducto(ItemCarrito nuevoItem)
    {
        var existente = carrito.FirstOrDefault(i => i.ProductoId == nuevoItem.ProductoId);
        if (existente is not null)
        {
            existente.Cantidad += nuevoItem.Cantidad;
        }
        else
        {
            carrito.Add(nuevoItem);
        }

        NotificarCambio();
    }

    public void EliminarProducto(int productoId)
    {
        var item = carrito.FirstOrDefault(i => i.ProductoId == productoId);
        if (item is not null)
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