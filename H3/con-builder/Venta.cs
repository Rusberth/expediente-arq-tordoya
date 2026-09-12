namespace ElAhorro.ConBuilder;

public class Venta
{
    public DateTime Fecha { get; init; } = DateTime.Now;
    public string MetodoPago { get; init; } = "";
    public string Estado { get; init; } = "";
    public string? NombreCliente { get; init; }
    public List<DetalleVenta> Detalles { get; init; } = new();
    public decimal Total { get; set; }

    public decimal CalcularTotal()
    {
        Total = Detalles.Sum(d => d.CalcularSubtotal());
        return Total;
    }
}
