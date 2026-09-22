// Refactor: <Jharol Rusberth Tordoya Sejas>
// Cura la violación de OCP detectada en detecciones.md: el switch por tipo de vehículo

namespace Integradora.Parqueo;

public interface IEstrategiaDeTarifa
{
    decimal CalcularTarifaPorHora();
}

public class TarifaAuto : IEstrategiaDeTarifa
{
    public decimal CalcularTarifaPorHora() => 5m;
}

public class TarifaMoto : IEstrategiaDeTarifa
{
    public decimal CalcularTarifaPorHora() => 3m;
}

public class TarifaResidente : IEstrategiaDeTarifa
{
    public decimal CalcularTarifaPorHora() => 1m;
}

public class GestorDeEstadias
{
    // antes recibia tipoVehiculo como texto y decidia la tarifa con un switch.
    // ahora recibe la estrategia ya elegida, no pregunta de qu tipo es el vehlculo

    public void RegistrarSalida(string placa, string tipoVehiculo, int horas, IEstrategiaDeTarifa estrategiaDeTarifa)
    {
        decimal tarifaPorHora = estrategiaDeTarifa.CalcularTarifaPorHora();
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
    public void Enviar(string mensaje) => Console.WriteLine($"[WHATSAPP] {mensaje}");
}

public static class Demo
{
    public static void Correr()
    {

        new GestorDeEstadias().RegistrarSalida("1234-ABC", "auto", 3, new TarifaAuto());
        new GestorDeEstadias().RegistrarSalida("9988-MOT", "moto", 5, new TarifaMoto());
        new GestorDeEstadias().RegistrarSalida("RES-014", "residente", 24, new TarifaResidente());
    }
}
