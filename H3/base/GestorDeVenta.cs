namespace ElAhorro.Base;

// Coordinador simple, sin ningún patrón todavía: numera la venta con un
// contador propio, descuenta stock y calcula el total. Sobre una copia de
// estas 4 clases se le va a aplicar cada patrón, uno por vez, en su propia carpeta.
public class GestorDeVenta
{
    private int _correlativo = 0;

    public Venta RegistrarVenta(List<DetalleVenta> detalles, string metodoPago)
    {
        _correlativo++;
        var venta = new Venta
        {
            Id = _correlativo,
            Detalles = detalles,
            MetodoPago = metodoPago
        };
        venta.CalcularTotal();

        foreach (var detalle in detalles)
            detalle.Producto.DescontarStock(detalle.Cantidad);

        Console.WriteLine($"[VENTA #{venta.Id}] Total: {venta.Total:0.00} Bs — pago: {venta.MetodoPago}");
        return venta;
    }
}

public static class Demo
{
    public static void Correr()
    {
        var arroz = new Producto { Id = 1, Nombre = "Arroz 1kg", PrecioVenta = 8.5m, StockActual = 20, StockMinimo = 5 };
        var gaseosa = new Producto { Id = 2, Nombre = "Gaseosa 2L", PrecioVenta = 12m, StockActual = 15, StockMinimo = 5 };

        var detalles = new List<DetalleVenta>
        {
            new() { Producto = arroz, Cantidad = 2, PrecioUnitario = arroz.PrecioVenta },
            new() { Producto = gaseosa, Cantidad = 1, PrecioUnitario = gaseosa.PrecioVenta }
        };

        new GestorDeVenta().RegistrarVenta(detalles, "efectivo");
    }
}
