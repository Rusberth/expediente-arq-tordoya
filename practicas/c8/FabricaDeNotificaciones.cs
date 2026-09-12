// RF5: avisar al Encargado cuando un producto cruza su stock minimo. SIN la
// fabrica, la decisioon "¿por que canal aviso?" quedaria copiada en cada lugar
// del sistema que necesite notificar. CON la fabrica, esa decision vive en
// UN solo lugar: aqui

namespace ElAhorro.C8;


public interface ICanalDeNotificacion
{
    void Enviar(string mensaje, string destinatario);
}

public class NotificadorPorCorreo : ICanalDeNotificacion
{
    public void Enviar(string mensaje, string destinatario)
        => Console.WriteLine($"[CORREO] {mensaje} — enviado a {destinatario}");
}

public class NotificadorPorWhatsApp : ICanalDeNotificacion
{
    public void Enviar(string mensaje, string destinatario)
        => Console.WriteLine($"[WHATSAPP] 📱 {mensaje} — enviado a {destinatario}");
}

public class NotificadorPorSms : ICanalDeNotificacion
{
    public void Enviar(string mensaje, string destinatario)
        => Console.WriteLine($"[SMS] {mensaje} — enviado a {destinatario}");
}

// LA FABRICA: la ventanilla única de avisos de "El Ahorro". Si el Encargado
// mañana pide un canal nuevo (ej. notificación push), se agrega UNA clase
// más y se toca SOLO esta linea del switch nada mas en todo el sistema.
public static class FabricaDeNotificaciones
{
    public static ICanalDeNotificacion Crear(string canal) => canal switch
    {
        "correo" => new NotificadorPorCorreo(),
        "whatsapp" => new NotificadorPorWhatsApp(),
        "sms" => new NotificadorPorSms(),
        _ => throw new ArgumentException($"Canal de aviso desconocido: {canal}")
    };
}
