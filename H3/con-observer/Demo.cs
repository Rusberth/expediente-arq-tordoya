namespace ElAhorro.ConObserver;

public static class Demo
{
    // Solucion: Jharol Rusberth Tordoya Sejas
    public static void Correr()
    {
        var arroz = new Producto { Nombre = "Arroz 1kg", StockActual = 12, StockMinimo = 10 };

        var control = new ControlDeStock();
        var registro = new RegistroDeAlertasDeStock();
        control.Suscribir(new NotificadorAlEncargado());
        control.Suscribir(registro);

        control.DescontarPorVenta(arroz, 1);
        control.DescontarPorVenta(arroz, 2);

        registro.ImprimirHistorial();
    }
}
