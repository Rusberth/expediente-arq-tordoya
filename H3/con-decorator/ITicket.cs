namespace ElAhorro.ConDecorator;

// cualquier cosa que se pueda imprimir como ticket de venta.
public interface ITicket
{
    void Imprimir(decimal totalVenta);
}
