namespace ElAhorro.ConSingleton;

public class Venta
{
    public int Id { get; set; }
    public List<DetalleVenta> Detalles { get; set; } = new();
    public decimal Total { get; set; }

    public decimal CalcularTotal()
    {
        Total = Detalles.Sum(d => d.CalcularSubtotal());
        return Total;
    }
}
