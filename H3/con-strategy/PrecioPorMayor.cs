namespace ElAhorro.ConStrategy;

public class PrecioPorMayor : IEstrategiaDePrecio
{
    private const int UmbralMayor = 12;
    private const decimal DescuentoMayor = 0.05m;

    public decimal CalcularSubtotal(decimal precioDeCatalogo, int cantidad)
    {
        var subtotal = precioDeCatalogo * cantidad;
        return cantidad >= UmbralMayor ? subtotal * (1 - DescuentoMayor) : subtotal;
    }
}
