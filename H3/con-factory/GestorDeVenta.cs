namespace ElAhorro.ConFactory;

// El gestor ya no decide CÓMO avisar: le pide el aviso a la fábrica y usa
// el contrato ICanalDeNotificacion. No sabe (ni le importa) qué canal le tocó.
public class GestorDeVenta
{
    private int _correlativo = 0;
    private readonly string _canalPreferidoDelEncargado;

    public GestorDeVenta(string canalPreferidoDelEncargado)
    {
        _canalPreferidoDelEncargado = canalPreferidoDelEncargado;
    }

    public Venta RegistrarVenta(List<DetalleVenta> detalles, string metodoPago)
    {
        _correlativo++;
        var venta = new Venta { Id = _correlativo, Detalles = detalles, MetodoPago = metodoPago };
        venta.CalcularTotal();

        foreach (var detalle in detalles)
        {
            detalle.Producto.DescontarStock(detalle.Cantidad);

            if (detalle.Producto.EstaBajoMinimo())
            {
                var canal = FabricaDeNotificaciones.Crear(_canalPreferidoDelEncargado);
                canal.Enviar($"Stock bajo: {detalle.Producto.Nombre} quedó en {detalle.Producto.StockActual}", "Encargado");
            }
        }

        Console.WriteLine($"[VENTA #{venta.Id}] Total: {venta.Total:0.00} Bs");
        return venta;
    }
}

public static class Demo
{
    public static void Correr()
    {
        var arroz = new Producto { Id = 1, Nombre = "Arroz 1kg", PrecioVenta = 8.5m, StockActual = 6, StockMinimo = 5 };

        var detalles = new List<DetalleVenta>
        {
            new() { Producto = arroz, Cantidad = 2, PrecioUnitario = arroz.PrecioVenta }
        };

        // El encargado eligió "whatsapp" como canal preferido — el gestor ni sabe
        // que existen NotificadorPorCorreo o NotificadorPorSms.
        var gestor = new GestorDeVenta("whatsapp");
        gestor.RegistrarVenta(detalles, "efectivo"); // deja el arroz en 4 → bajo el mínimo → dispara el aviso

        Console.WriteLine("El canal se resolvió en UN solo lugar (la fábrica). El gestor no cambió una línea.");
    }
}
