namespace ElAhorro.Final;

public static class Demo
{
    // Solucion: Jharol Rusberth Tordoya Sejas
    public static void Correr()
    {
        var gestor = new GestorDeCierreDeVenta(new AdaptadorFacturacionSIN());

        var arroz = new Producto { Nombre = "Arroz 1kg", PrecioVenta = 8.50m };
        var aceite = new Producto { Nombre = "Aceite 1L", PrecioVenta = 12.00m };

        // Combinación 1: venta al contado, con factura.
        var armador1 = new ArmadorDeVenta().AgregarProducto(arroz, 3).ConMetodoPago("efectivo");
        var venta1 = gestor.CerrarVenta(armador1, nitOCiCliente: "1234567");
        Console.WriteLine($"Venta #{venta1.Id} cerrada. Total: Bs {venta1.Total:0.00}");

        Console.WriteLine();

        // Combinación 2: venta fiada — el Builder exige cliente en vez de método de pago;
        // el Adapter no sabe ni le importa si fue fiada o al contado.
        var armador2 = new ArmadorDeVenta().AgregarProducto(aceite, 2).ComoFiado("Doña Rosa");
        var venta2 = gestor.CerrarVenta(armador2, nitOCiCliente: "7654321");
        Console.WriteLine($"Venta #{venta2.Id} cerrada (fiada a {venta2.NombreCliente}). Total: Bs {venta2.Total:0.00}");
    }
}
