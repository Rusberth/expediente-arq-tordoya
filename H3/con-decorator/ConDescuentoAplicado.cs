namespace ElAhorro.ConDecorator;

// Capa 1: si la venta llevo descuento, el ticket lo muestra desglosado.
public class ConDescuentoAplicado : CapaDeTicket
{
    private readonly decimal _montoDescontado;

    public ConDescuentoAplicado(ITicket interno, decimal montoDescontado) : base(interno)
        => _montoDescontado = montoDescontado;

    public override void Imprimir(decimal totalVenta)
    {
        Interno.Imprimir(totalVenta);
        Console.WriteLine($"   ↳ descuento aplicado: -Bs {_montoDescontado:0.00}");
    }
}
