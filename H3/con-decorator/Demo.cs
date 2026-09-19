namespace ElAhorro.ConDecorator;

public static class Demo
{
    // Solucion: Jharol Rusberth Tordoya Sejas
    public static void Correr()
    {
        // Combinación 1: venta con descuento y con factura — se arma en la llamada.
        ITicket completo = new ConDatosDeFactura(
            new ConDescuentoAplicado(new TicketBase(), montoDescontado: 4.25m),
            nit: "1234567");
        completo.Imprimir(80.75m);

        Console.WriteLine();
        Console.WriteLine("-- otra combinación: solo descuento, cero clases nuevas --");
        ITicket soloDescuento = new ConDescuentoAplicado(new TicketBase(), montoDescontado: 2.00m);
        soloDescuento.Imprimir(38.00m);
    }
}
