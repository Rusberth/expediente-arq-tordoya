// PARCIAL 1 · VARIANTE B — Ferretería "El Tornillo"
// P1.2 — Cura de DOS violaciones SOLID: ISP y DIP

namespace Parcial1.Ferreteria.Refactor;

// ============================================================
// CURA 1 — ISP 
// ------------------------------------------------------------
// Problema original: IEmpleadoDeFerreteria obligaba a Vendedor a implementar
// AutorizarVentaAlPorMayor, AjustarPrecio y VerReporteDeCompras, metodos que
// no le corresponden, resolviendo con NotSupportedException.
//
// Solucion: partir la interfaz gorda en contratos pequeños, uno por
// responsabilidad.
// ============================================================

public interface IRegistraPedidos
{
    void RegistrarPedido(string material, int cantidad);
}

public interface IAutorizaVentasAlPorMayor
{
    void AutorizarVentaAlPorMayor(string material);
}

public interface IAjustaPrecios
{
    void AjustarPrecio(string material, decimal nuevoPrecio);
}

public interface IConsultaReportes
{
    void VerReporteDeCompras();
}

public class Encargado : IRegistraPedidos, IAutorizaVentasAlPorMayor, IAjustaPrecios, IConsultaReportes
{
    public void RegistrarPedido(string material, int cantidad)
        => Console.WriteLine($"[ENC] Pedido: {cantidad} x {material}");

    public void AutorizarVentaAlPorMayor(string material)
        => Console.WriteLine($"[ENC] Venta al por mayor de {material} autorizada");

    public void AjustarPrecio(string material, decimal nuevoPrecio)
        => Console.WriteLine($"[ENC] {material} ahora cuesta {nuevoPrecio:0.00} Bs");

    public void VerReporteDeCompras()
        => Console.WriteLine("[ENC] Reporte de compras del mes");
}

public class Vendedor : IRegistraPedidos
{
    public void RegistrarPedido(string material, int cantidad)
        => Console.WriteLine($"[VEND] Pedido: {cantidad} x {material}");
}

// ya no existe NotSupportedException en ningún lado. Vendedor solo
// declara (e implementa) el contrato que le corresponde: IRegistraPedidos.

// ============================================================
// CURA 2 — DIP
// ------------------------------------------------------------
// Problema original: GestorDePedidos.ProcesarPedido hacia
// "new BaseDeDatosMySql()" y "new CorreoSmtp()" dentro del método: el mdulo
// dependia directamente de modulos concretos.
//
// Solucion: GestorDePedidos depende de abstracciones (IRepositorioPedidos,
// INotificador) que recibe por constructor.
// ============================================================

public interface IRepositorioPedidos
{
    void GuardarPedido(string cliente, string material, int cantidad, decimal total);
}

public interface INotificador
{
    void Enviar(string mensaje);
}

public class BaseDeDatosMySql : IRepositorioPedidos
{
    public void GuardarPedido(string cliente, string material, int cantidad, decimal total)
        => Console.WriteLine($"[MYSQL] INSERT INTO pedidos VALUES ('{cliente}', '{material}', {cantidad}, {total})");
}

public class CorreoSmtp : INotificador
{
    public void Enviar(string mensaje)
        => Console.WriteLine($"[SMTP] {mensaje}");
}

public class GestorDePedidos
{
    private readonly IRepositorioPedidos _repositorio;
    private readonly INotificador _notificador;

    // Constructor que recibe contratos, no implementaciones concretas.
    public GestorDePedidos(IRepositorioPedidos repositorio, INotificador notificador)
    {
        _repositorio = repositorio;
        _notificador = notificador;
    }

    public void ProcesarPedido(string cliente, string tipoCliente, string material, int cantidad, decimal precioUnitario)
    {
        decimal total = cantidad * precioUnitario;
        decimal descuento;
        switch (tipoCliente)
        {
            case "particular":
                descuento = 0;
                break;
            case "contratista":
                descuento = total * 0.15m;
                break;
            case "constructora":
                descuento = total * 0.25m;
                break;
            default:
                descuento = 0;
                break;
        }
        decimal totalFinal = total - descuento;

        _repositorio.GuardarPedido(cliente, material, cantidad, totalFinal);

        Console.WriteLine("----- COMPROBANTE -----");
        Console.WriteLine($"{cantidad} x {material}");
        Console.WriteLine($"Cliente: {cliente} ({tipoCliente})");
        Console.WriteLine($"TOTAL: {totalFinal:0.00} Bs");

        _notificador.Enviar($"Su pedido de {material} fue registrado, {cliente}");
    }
}

public static class Demo
{
    public static void Correr()
    {
        // las dependencias se arman aqiu,
        var gestor = new GestorDePedidos(new BaseDeDatosMySql(), new CorreoSmtp());
        gestor.ProcesarPedido("Marco", "contratista", "Cemento 50kg", 10, 62.00m);
    }
}
