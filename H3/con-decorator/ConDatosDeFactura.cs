namespace ElAhorro.ConDecorator;

// Capa 2: si el cliente pidio factura, se agrega el NIT  sin tocar el ticket base.
public class ConDatosDeFactura : CapaDeTicket
{
    private readonly string _nit;

    public ConDatosDeFactura(ITicket interno, string nit) : base(interno)
        => _nit = nit;

    public override void Imprimir(decimal totalVenta)
    {
        Interno.Imprimir(totalVenta);
        Console.WriteLine($"   ↳ factura a nombre de NIT {_nit}");
    }
}
