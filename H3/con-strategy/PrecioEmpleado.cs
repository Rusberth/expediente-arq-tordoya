namespace ElAhorro.ConStrategy;

public class PrecioEmpleado : IEstrategiaDePrecio
{
    private const decimal DescuentoEmpleado = 0.10m;

    public decimal CalcularSubtotal(decimal precioDeCatalogo, int cantidad)
        => precioDeCatalogo * cantidad * (1 - DescuentoEmpleado);
}
