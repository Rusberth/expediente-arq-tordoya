// H3 · con-adapter — el sistema EXTERNO que toca mi dominio: el SIN (Sistema
// de Facturación Virtual de Bolivia). "El Ahorro" no controla cómo habla el
// SIN — es de terceros, no se puede tocar — y el SIN no sabe nada de mis
// clases Venta/DetalleVenta. El traductor convierte EN LA FRONTERA, para que
// mi GestorDeVenta hable siempre en su propio idioma.

namespace ElAhorro.ConAdapter;

// EL CONTRATO: "El Ahorro" define en SU idioma qué necesita para facturar.
public interface IServicioDeFacturacion
{
    string EmitirFactura(Venta venta, string nitOCiCliente);
}

// EL SISTEMA EXTERNO (simulado): el SIN habla en su propio formato — pide un
// monto, un NIT y una fecha, y devuelve un CUF (Código Único de Facturación).
// Intocable: así lo definió la entidad reguladora, no nosotros.
public class SistemaDeFacturacionSIN
{
    public string GenerarCuf(decimal montoTotal, string nit, DateTime fechaEmision)
        => $"SIN-CUF-{nit}-{fechaEmision:yyyyMMdd}-{montoTotal:0000}";
}

// EL TRADUCTOR: el único lugar del sistema que sabe hablar con el SIN.
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

// Alternativa para "sin factura" (venta informal con NIT genérico "0"):
// una nota de venta simple, SIN tocar el SIN. Otro traductor, mismo contrato.
public class NotaDeVentaSimple : IServicioDeFacturacion
{
    public string EmitirFactura(Venta venta, string nitOCiCliente)
    {
        string numero = $"NV-{venta.Id:0000}";
        Console.WriteLine($"[NOTA DE VENTA] Venta #{venta.Id} — comprobante interno: {numero}");
        return numero;
    }
}

public static class Demo
{
    public static void Correr()
    {
        var arroz = new Producto { Id = 1, Nombre = "Arroz 1kg", PrecioVenta = 8.5m, StockActual = 20 };
        var venta = new Venta { Id = 1, Detalles = new() { new() { Producto = arroz, Cantidad = 2, PrecioUnitario = arroz.PrecioVenta } } };
        venta.CalcularTotal();

        IServicioDeFacturacion facturador = new AdaptadorFacturacionSIN();
        facturador.EmitirFactura(venta, "1234567015");

        // Si el cliente no pide factura, se usa OTRO traductor — el resto del
        // sistema (quien llama a IServicioDeFacturacion) no cambia ni una línea.
        IServicioDeFacturacion notaSimple = new NotaDeVentaSimple();
        notaSimple.EmitirFactura(venta, "0");

        Console.WriteLine("Dos formas de emitir comprobante, UN mismo contrato. El SIN nunca tocó mi dominio.");
    }
}
