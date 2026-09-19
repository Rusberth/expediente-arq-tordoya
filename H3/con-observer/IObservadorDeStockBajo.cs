namespace ElAhorro.ConObserver;

// El contrato: todo interesado en el stock bajo sabe reaccionar cuando ocurre.
public interface IObservadorDeStockBajo
{
    void CuandoStockBajo(Producto producto);
}
