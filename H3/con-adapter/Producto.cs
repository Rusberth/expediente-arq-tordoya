namespace ElAhorro.ConAdapter;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public decimal PrecioVenta { get; set; }
    public int StockActual { get; set; }

    public void DescontarStock(int cantidad) => StockActual -= cantidad;
}
