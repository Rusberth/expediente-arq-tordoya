namespace ElAhorro.Final;

// MI contrato: en mi idioma, recibe una Venta.
public interface IServicioDeFacturacion
{
    string EmitirFactura(Venta venta, string nitOCiCliente);
}

// El sistema externo, intocable — habla en su idioma.
public class SistemaDeFacturacionSIN
{
    public string GenerarCuf(decimal montoTotal, string nit, DateTime fechaEmision)
        => $"SIN-CUF-{nit}-{fechaEmision:yyyyMMdd}-{montoTotal:0000}";
}

// El traductor.
public class AdaptadorFacturacionSIN : IServicioDeFacturacion
{
    private readonly SistemaDeFacturacionSIN _sin = new();

    public string EmitirFactura(Venta venta, string nitOCiCliente)
    {
        string cuf = _sin.GenerarCuf(venta.Total, nitOCiCliente, venta.Fecha);
        Console.WriteLine($"[FACTURACIÓN SIN] Venta #{venta.Id} — CUF: {cuf}");
        return cuf;
    }
}
