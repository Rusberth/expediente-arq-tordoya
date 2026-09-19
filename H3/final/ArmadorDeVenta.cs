namespace ElAhorro.Final;

public class ArmadorDeVenta
{
    private static int _correlativo = 0;
    private readonly List<DetalleVenta> _detalles = new();
    private string _metodoPago = "";
    private bool _esFiado;
    private string? _nombreCliente;

    public ArmadorDeVenta AgregarProducto(Producto producto, int cantidad)
    {
        _detalles.Add(new DetalleVenta { Producto = producto, Cantidad = cantidad });
        return this;
    }

    public ArmadorDeVenta ConMetodoPago(string metodoPago) { _metodoPago = metodoPago; return this; }

    public ArmadorDeVenta ComoFiado(string nombreCliente)
    {
        _esFiado = true;
        _nombreCliente = nombreCliente;
        return this;
    }

    public Venta Construir()
    {
        if (_detalles.Count == 0)
            throw new InvalidOperationException("Falta agregar al menos un producto.");
        if (!_esFiado && string.IsNullOrEmpty(_metodoPago))
            throw new InvalidOperationException("Falta el método de pago (o marcarla como fiado).");
        if (_esFiado && string.IsNullOrEmpty(_nombreCliente))
            throw new InvalidOperationException("Una venta fiada necesita el nombre del cliente.");

        return new Venta
        {
            Id = ++_correlativo,
            Detalles = _detalles,
            MetodoPago = _metodoPago,
            EsFiado = _esFiado,
            NombreCliente = _nombreCliente
        };
    }
}
