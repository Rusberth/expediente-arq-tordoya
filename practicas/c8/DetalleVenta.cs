namespace ElAhorro.C8;

public class DetalleVenta
{
    public Producto Producto { get; set; } = null!;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public decimal CalcularSubtotal() => Cantidad * PrecioUnitario;
}
