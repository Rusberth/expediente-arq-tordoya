namespace ElAhorro.ConDecorator;

// solo el total.
public class TicketBase : ITicket
{
    public void Imprimir(decimal totalVenta)
        => Console.WriteLine($"[TICKET] Total a pagar: Bs {totalVenta:0.00}");
}
