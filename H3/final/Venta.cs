namespace ElAhorro.Final;

public class Venta
{
    public int Id { get; init; }
    public DateTime Fecha { get; init; } = DateTime.Now;
    public List<DetalleVenta> Detalles { get; init; } = new();
    public string MetodoPago { get; init; } = "";
    public bool EsFiado { get; init; }
    public string? NombreCliente { get; init; }
    public decimal Total => Detalles.Sum(d => d.Subtotal);
}
