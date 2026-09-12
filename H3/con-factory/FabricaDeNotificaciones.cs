// H3 · con-factory — RF5: avisar al Encargado cuando un producto cruza su
// stock mínimo. SIN la fábrica, la decisión "¿por qué canal aviso?" quedaría
// copiada en cada lugar del sistema que necesite notificar (igual que
// SecretariaDeNotas y DireccionDeCitaciones en el ejemplo de clase). CON la
// fábrica, esa decisión vive en UN solo lugar: acá.

namespace ElAhorro.ConFactory;

// El contrato: cualquier canal de aviso sabe enviarse a sí mismo.
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

// LA FÁBRICA: la ventanilla única de avisos de "El Ahorro". Si el Encargado
// mañana pide un canal nuevo (ej. notificación push), se agrega UNA clase
// más y se toca SOLO esta línea del switch — nada más en todo el sistema.
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
