namespace ElAhorro.ConStrategy;

public interface IEstrategiaDePrecio
{
    decimal CalcularSubtotal(decimal precioDeCatalogo, int cantidad);
}
