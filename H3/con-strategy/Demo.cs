namespace ElAhorro.ConStrategy;

public static class Demo
{
    // Solucion: Jharol Rusberth Tordoya Sejas
    public static void Correr()
    {
        var calculadora = new CalculadoraDeSubtotal();
        decimal precioArroz = 8.50m;

        Console.WriteLine($"[DETALLE]  3 kg de arroz:  Bs {calculadora.Calcular(precioArroz, 3, new PrecioAlDetalle()):0.00}");
        Console.WriteLine($"[MAYOR]    20 kg de arroz: Bs {calculadora.Calcular(precioArroz, 20, new PrecioPorMayor()):0.00}");
        Console.WriteLine($"[EMPLEADO] 3 kg de arroz:  Bs {calculadora.Calcular(precioArroz, 3, new PrecioEmpleado()):0.00}");
    }
}
