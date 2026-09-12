namespace ElAhorro.Base;

public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public string MetodoPago { get; set; } = "";
    public string Estado { get; set; } = "confirmada";
    public List<DetalleVenta> Detalles { get; set; } = new();
    public decimal Total { get; set; }

    public decimal CalcularTotal()
    {
        Total = Detalles.Sum(d => d.CalcularSubtotal());
        return Total;
    }
}
