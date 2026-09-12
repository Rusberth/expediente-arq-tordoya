// H3 · con-builder — el objeto complejo que elegí de mi dominio: UNA VENTA.
// Antes (sin este patrón) armar una Venta a mano significaba acordarse de
// crear la lista de detalles, decidir el método de pago, y por separado
// manejar el caso "fiado" (RF de crédito a clientes de confianza) — con el
// riesgo de dejarla "pagada" sin método de pago, o "fiada" sin saber a quién
// cobrarle. El armador convierte eso en pasos con nombre, y Construir() es
// el guardián: una venta incompleta NI SIQUIERA nace.

namespace ElAhorro.ConBuilder;

public class ArmadorDeVenta
{
    private readonly List<DetalleVenta> _detalles = new();
    private string _metodoPago = "";
    private bool _esFiado;
    private string? _nombreCliente;

    public ArmadorDeVenta AgregarProducto(Producto producto, int cantidad)
    {
        _detalles.Add(new DetalleVenta { Producto = producto, Cantidad = cantidad, PrecioUnitario = producto.PrecioVenta });
        return this;
    }

    public ArmadorDeVenta ConMetodoPago(string metodoPago)
    {
        _metodoPago = metodoPago;
        return this;
    }

    public ArmadorDeVenta ComoFiado(string nombreCliente)
    {
        _esFiado = true;
        _nombreCliente = nombreCliente;
        return this;
    }

    // EL GUARDIÁN: revisa antes de entregar. Nada nace incompleto.
    public Venta Construir()
    {
        if (_detalles.Count == 0)
            throw new InvalidOperationException("Falta agregar al menos un producto: no hay venta sin productos.");
        if (!_esFiado && string.IsNullOrEmpty(_metodoPago))
            throw new InvalidOperationException("Falta el método de pago (o marcarla como fiado con .ComoFiado()).");
        if (_esFiado && string.IsNullOrEmpty(_nombreCliente))
            throw new InvalidOperationException("Una venta fiada necesita el nombre del cliente.");

        var venta = new Venta
        {
            Detalles = _detalles,
            MetodoPago = _esFiado ? "fiado" : _metodoPago,
            Estado = _esFiado ? "pendiente de pago" : "pagada",
            NombreCliente = _nombreCliente
        };
        venta.CalcularTotal();

        foreach (var detalle in _detalles)
            detalle.Producto.DescontarStock(detalle.Cantidad);

        return venta;
    }
}

public static class Demo
{
    public static void Correr()
    {
        var arroz = new Producto { Id = 1, Nombre = "Arroz 1kg", PrecioVenta = 8.5m, StockActual = 20 };
        var gaseosa = new Producto { Id = 2, Nombre = "Gaseosa 2L", PrecioVenta = 12m, StockActual = 15 };

        // La venta se LEE como una oración — imposible confundir "cantidad" con "precio":
        var ventaAlContado = new ArmadorDeVenta()
            .AgregarProducto(arroz, 2)
            .AgregarProducto(gaseosa, 1)
            .ConMetodoPago("efectivo")
            .Construir();
        Console.WriteLine($"[VENTA] Total {ventaAlContado.Total:0.00} Bs — estado: {ventaAlContado.Estado}");

        var ventaFiada = new ArmadorDeVenta()
            .AgregarProducto(arroz, 1)
            .ComoFiado("Don Ramiro")
            .Construir();
        Console.WriteLine($"[VENTA] Total {ventaFiada.Total:0.00} Bs — estado: {ventaFiada.Estado} — cliente: {ventaFiada.NombreCliente}");

        // El guardián en acción: una venta sin productos NO nace.
        try
        {
            new ArmadorDeVenta().ConMetodoPago("efectivo").Construir();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"[GUARDIÁN] 🛑 {e.Message}");
        }

        // El guardián también atrapa el fiado sin cliente:
        try
        {
            new ArmadorDeVenta().AgregarProducto(gaseosa, 1).ComoFiado("").Construir();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"[GUARDIÁN] 🛑 {e.Message}");
        }
    }
}
