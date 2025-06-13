namespace servidor.DTOs;

public class CompraDto
{
    public string NombreCliente { get; set; } = string.Empty;
    public string ApellidoCliente { get; set; } = string.Empty;
    public string EmailCliente { get; set; } = string.Empty;

    public List<ItemCompraDto> Items { get; set; } = new();
}

public class ItemCompraDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}