// Mi candidato a Singleton: el correlativo de boletas de todo el minimarket.

namespace ElAhorro.C8;

public class GeneradorDeCorrelativoDeVenta
{
    private static GeneradorDeCorrelativoDeVenta? _instancia;
    private int _ultimoNumero = 0;

    private GeneradorDeCorrelativoDeVenta() { } // el candado: nadie más hace new

    public static GeneradorDeCorrelativoDeVenta Instancia
        => _instancia ??= new GeneradorDeCorrelativoDeVenta();

    public int SiguienteNumero()
    {
        _ultimoNumero++;
        return _ultimoNumero;
    }
}
