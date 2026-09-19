namespace ElAhorro.ConObserver;

// El SUJETO: mantiene la lista de interesados y los avisa. No conoce a
// NotificadorAlEncargado ni a RegistroDeAlertasDeStock por nombre.
public class ControlDeStock
{
    private readonly List<IObservadorDeStockBajo> _observadores = new();

    public void Suscribir(IObservadorDeStockBajo observador)
        => _observadores.Add(observador);

    public void DescontarPorVenta(Producto producto, int cantidad)
    {
        producto.StockActual -= cantidad;
        Console.WriteLine($"[STOCK] {producto.Nombre}: {producto.StockActual} u. restantes.");

        if (producto.StockActual <= producto.StockMinimo)
            foreach (var observador in _observadores)
                observador.CuandoStockBajo(producto);
    }
}
