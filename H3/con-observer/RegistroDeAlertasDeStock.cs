namespace ElAhorro.ConObserver;

// Observador 2: el módulo de Reportes (RF6), que necesita el historial de alertas.
public class RegistroDeAlertasDeStock : IObservadorDeStockBajo
{
    private readonly List<string> _alertas = new();

    public void CuandoStockBajo(Producto producto)
        => _alertas.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} — {producto.Nombre} bajo mínimo ({producto.StockActual}/{producto.StockMinimo})");

    public void ImprimirHistorial()
    {
        Console.WriteLine("[REGISTRO DE ALERTAS]");
        foreach (var a in _alertas) Console.WriteLine($"  {a}");
    }
}
