// H3 · con-singleton — mi candidato a Singleton: el correlativo de boletas
// de TODO el minimarket. Ver justificacion.md para el razonamiento completo
// (el "test del arquitecto": ¿el mundo real permite DOS?).

namespace ElAhorro.ConSingleton;

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
