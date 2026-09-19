namespace ElAhorro.ConObserver;

// Observador 1: el Encargado — es quien tiene autoridad para reponer (RF5).
public class NotificadorAlEncargado : IObservadorDeStockBajo
{
    public void CuandoStockBajo(Producto producto)
        => Console.WriteLine($"[AVISO ENCARGADO] {producto.Nombre} llegó a {producto.StockActual} u. (mínimo {producto.StockMinimo}) — hay que reponer.");
}
