namespace ElAhorro.Final;

// Aqui conviven los dos patrones: recibe el armador YA CARGADO (Builder) y,
// si la venta es valida, la manda a facturar al sistema externo (Adapter).
// Un solo metodo, los dos patrones trabajando juntos.
public class GestorDeCierreDeVenta
{
    private readonly IServicioDeFacturacion _facturacion;

    public GestorDeCierreDeVenta(IServicioDeFacturacion facturacion)
        => _facturacion = facturacion;

    public Venta CerrarVenta(ArmadorDeVenta armador, string nitOCiCliente)
    {
        Venta venta = armador.Construir();                 // BUILDER: arma y valida
        _facturacion.EmitirFactura(venta, nitOCiCliente);   // ADAPTER: traduce y factura
        return venta;
    }
}
