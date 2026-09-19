namespace ElAhorro.Final;

public class DetalleVenta
{
    public Producto Producto { get; init; } = null!;
    public int Cantidad { get; init; }
    public decimal Subtotal => Producto.PrecioVenta * Cantidad;
}
