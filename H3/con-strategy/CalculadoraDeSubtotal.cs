namespace ElAhorro.ConStrategy;

public class CalculadoraDeSubtotal
{
    public decimal Calcular(decimal precioDeCatalogo, int cantidad, IEstrategiaDePrecio estrategia)
        => estrategia.CalcularSubtotal(precioDeCatalogo, cantidad);
}
