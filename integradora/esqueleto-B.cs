// INTEGRADORA · VARIANTE B — Parqueo del Edificio "Torre Central"
// Esqueleto del sistema de estadías del parqueo. FUNCIONA, pero fue escrito de apuro:
// tiene 3 violaciones SOLID. Es la materia prima de tu parte 2.

namespace Integradora.Parqueo;

public class GestorDeEstadias
{
    public void RegistrarSalida(string placa, string tipoVehiculo, int horas)
    {
        decimal tarifaPorHora;
        switch (tipoVehiculo)
        {
            case "auto":
                tarifaPorHora = 5;
                break;
            case "moto":
                tarifaPorHora = 3;
                break;
            case "residente":
                tarifaPorHora = 1;
                break;
            default:
                tarifaPorHora = 5;
                break;
        }
        decimal total = tarifaPorHora * horas;

        var baseDeDatos = new BaseDeDatosParqueo();
        baseDeDatos.GuardarEstadia(placa, tipoVehiculo, horas, total);

        Console.WriteLine("----- TICKET DE SALIDA -----");
        Console.WriteLine($"Placa {placa}: {horas} h como {tipoVehiculo}");
        Console.WriteLine($"TOTAL: {total:0.00} Bs");

        var whatsapp = new WhatsAppDelEdificio();
        whatsapp.Enviar($"Salida registrada: {placa}, {horas} h, {total:0.00} Bs");
    }
}

public class BaseDeDatosParqueo
{
    public void GuardarEstadia(string placa, string tipo, int horas, decimal total)
        => Console.WriteLine($"[BD] INSERT INTO estadias VALUES ('{placa}', '{tipo}', {horas}, {total})");
}

public class WhatsAppDelEdificio
{
    public void Enviar(string mensaje) => Console.WriteLine($"[WHATSAPP] 📱 {mensaje}");
}

public static class Demo
{
    public static void Correr()
    {
        new GestorDeEstadias().RegistrarSalida("1234-ABC", "auto", 3);
    }
}
