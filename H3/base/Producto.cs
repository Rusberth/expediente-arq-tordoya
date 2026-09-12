// H3 · BASE — el corazón del caso SIN patrones, transcrito del diagrama del H2.
// Esta clase (y las otras 3 de esta carpeta) es el punto de partida neutral:
// sobre una COPIA de estas mismas 4 clases se implementa cada patrón, aislado.

namespace ElAhorro.Base;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public decimal PrecioVenta { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }

    public void DescontarStock(int cantidad)
    {
        StockActual -= cantidad;
    }

    public bool EstaBajoMinimo() => StockActual <= StockMinimo;
}
