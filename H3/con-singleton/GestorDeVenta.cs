namespace ElAhorro.ConSingleton;

// El gestor ya NO lleva su propio contador: pide el número al generador
// único. Da igual desde qué caja se registre la venta — todas comparten
// la misma numeración oficial.
public class GestorDeVenta
{
    public Venta RegistrarVenta(List<DetalleVenta> detalles)
    {
        var venta = new Venta
        {
            Id = GeneradorDeCorrelativoDeVenta.Instancia.SiguienteNumero(),
            Detalles = detalles
        };
        venta.CalcularTotal();

        foreach (var d in detalles)
            d.Producto.DescontarStock(d.Cantidad);

        Console.WriteLine($"[VENTA #{venta.Id}] Total: {venta.Total:0.00} Bs");
        return venta;
    }
}

public static class Demo
{
    public static void Correr()
    {
        var arroz = new Producto { Id = 1, Nombre = "Arroz 1kg", PrecioVenta = 8.5m, StockActual = 20 };
        var gaseosa = new Producto { Id = 2, Nombre = "Gaseosa 2L", PrecioVenta = 12m, StockActual = 15 };

        // Dos GestorDeVenta distintos = dos cajas físicas distintas del minimarket.
        var cajaUno = new GestorDeVenta();
        var cajaDos = new GestorDeVenta();

        cajaUno.RegistrarVenta(new() { new() { Producto = arroz, Cantidad = 1, PrecioUnitario = arroz.PrecioVenta } });
        cajaDos.RegistrarVenta(new() { new() { Producto = gaseosa, Cantidad = 1, PrecioUnitario = gaseosa.PrecioVenta } });
        cajaUno.RegistrarVenta(new() { new() { Producto = arroz, Cantidad = 3, PrecioUnitario = arroz.PrecioVenta } });

        Console.WriteLine("Boletas #1, #2, #3 — sin choques, aunque salieron de DOS cajas distintas.");
        Console.WriteLine("Sin el Singleton, cada caja tendría su propio contador y ambas emitirían una boleta #1.");
    }
}
