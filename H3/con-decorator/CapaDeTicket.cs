namespace ElAhorro.ConDecorator;

// EL DECORADOR: ES un ITicket... y contiene otro ITickett
public abstract class CapaDeTicket : ITicket
{
    protected readonly ITicket Interno;
    protected CapaDeTicket(ITicket interno) => Interno = interno;
    public abstract void Imprimir(decimal totalVenta);
}
