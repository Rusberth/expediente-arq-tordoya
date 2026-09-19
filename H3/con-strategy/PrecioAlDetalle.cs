namespace ElAhorro.ConStrategy;

public class PrecioAlDetalle : IEstrategiaDePrecio
{
    public decimal CalcularSubtotal(decimal precioDeCatalogo, int cantidad)
        => precioDeCatalogo * cantidad;
}
