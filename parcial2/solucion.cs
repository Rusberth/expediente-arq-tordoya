// Solucion: Jharol Rusberth Tordoya Sejas
// PARCIAL 2 - Arquitectura de Software - Variante A (Biblioteca Municipal)
// P2.2 - Situacion 2: calculo de multas por atraso, distinto segun tipo de socio.
// Patron aplicado: STRATEGY
//
// Problema original: el calculo de la multa vivia en un if/else dentro del modulo
// de prestamos, Y estaba copiado en el modulo de reportes. Cada vez que el concejo
// municipal cambia las reglas, hay que tocar (y sincronizar) dos lugares distintos.
//
// Solucion: cada regla de calculo se encapsula en una clase que implementa el
// contrato ICalculadoraMulta. ModuloPrestamos y ModuloReportes ya no conocen las
// reglas: solo conocen el contrato y piden la calculadora correcta a un registro
// central. Agregar o cambiar una regla = agregar/tocar UNA clase, sin tocar
// ModuloPrestamos ni ModuloReportes.

using System;
using System.Collections.Generic;

namespace BibliotecaMunicipal.Multas
{

    public enum TipoSocio
    {
        Infantil,
        Adulto,
        TerceraEdad
    }

    public class Socio
    {
        public string Nombre { get; }
        public TipoSocio Tipo { get; }

        public Socio(string nombre, TipoSocio tipo)
        {
            Nombre = nombre;
            Tipo = tipo;
        }
    }

    public class Prestamo
    {
        public Socio Socio { get; }
        public string Titulo { get; }
        public int DiasAtraso { get; }

        public Prestamo(Socio socio, string titulo, int diasAtraso)
        {
            Socio = socio;
            Titulo = titulo;
            DiasAtraso = diasAtraso;
        }
    }

    // Strategy

    public interface ICalculadoraMulta
    {
        // monto a cobraar
        decimal CalcularMulta(int diasAtraso);

        // reglas de bloqueo 
        bool BloqueaNuevosPrestamos(int diasAtraso);
    }

 

    // Pieza 1: socio infantil no paga
    public class MultaInfantil : ICalculadoraMulta
    {
        public decimal CalcularMulta(int diasAtraso) => 0m;

        public bool BloqueaNuevosPrestamos(int diasAtraso) => diasAtraso > 0;
    }

    // Pieza 2: socio adulto 2 Bs por dia de atraso
    public class MultaAdulto : ICalculadoraMulta
    {
        private const decimal TarifaPorDia = 2m;

        public decimal CalcularMulta(int diasAtraso) =>
            diasAtraso <= 0 ? 0m : diasAtraso * TarifaPorDia;

        public bool BloqueaNuevosPrestamos(int diasAtraso) => false;
    }

    // Pieza 3: socio de tercera edad 1 Bs por dia
    public class MultaTerceraEdad : ICalculadoraMulta
    {
        private const decimal TarifaPorDia = 1m;
        private const decimal TopeMaximo = 20m;

        public decimal CalcularMulta(int diasAtraso) =>
            diasAtraso <= 0 ? 0m : Math.Min(diasAtraso * TarifaPorDia, TopeMaximo);

        public bool BloqueaNuevosPrestamos(int diasAtraso) => false;
    }

    // Registro central
    public static class RegistroCalculadorasMulta
    {
        private static readonly Dictionary<TipoSocio, ICalculadoraMulta> Calculadoras =
            new Dictionary<TipoSocio, ICalculadoraMulta>
            {
                { TipoSocio.Infantil, new MultaInfantil() },
                { TipoSocio.Adulto, new MultaAdulto() },
                { TipoSocio.TerceraEdad, new MultaTerceraEdad() }
            };

        public static ICalculadoraMulta Para(TipoSocio tipo) => Calculadoras[tipo];
    }

  

    public class ModuloPrestamos
    {
        public void RegistrarDevolucion(Prestamo prestamo)
        {
            ICalculadoraMulta calculadora = RegistroCalculadorasMulta.Para(prestamo.Socio.Tipo);

            decimal multa = calculadora.CalcularMulta(prestamo.DiasAtraso);
            bool bloqueado = calculadora.BloqueaNuevosPrestamos(prestamo.DiasAtraso);

            Console.WriteLine(
                $"[Prestamos] {prestamo.Socio.Nombre} ({prestamo.Socio.Tipo}) devuelve '{prestamo.Titulo}' " +
                $"con {prestamo.DiasAtraso} dia(s) de atraso -> multa: {multa} Bs. " +
                $"Bloqueado para nuevos prestamos: {bloqueado}");
        }
    }

    public class ModuloReportes
    {
        public void GenerarReporteMultas(IEnumerable<Prestamo> prestamos)
        {
            Console.WriteLine("[Reportes] Resumen de multas del periodo:");
            decimal totalRecaudado = 0m;

            foreach (var prestamo in prestamos)
            {
                ICalculadoraMulta calculadora = RegistroCalculadorasMulta.Para(prestamo.Socio.Tipo);
                decimal multa = calculadora.CalcularMulta(prestamo.DiasAtraso);
                totalRecaudado += multa;

                Console.WriteLine($"  - {prestamo.Socio.Nombre}: {multa} Bs");
            }

            Console.WriteLine($"  Total a recaudar: {totalRecaudado} Bs");
        }
    }

    // ejemplo

    public class Program
    {
        public static void Main()
        {
            var ana = new Socio("Ana Quispe", TipoSocio.Infantil);
            var carlos = new Socio("Carlos Mamani", TipoSocio.Adulto);
            var donaRosa = new Socio("Rosa Fernandez", TipoSocio.TerceraEdad);

            var prestamos = new List<Prestamo>
            {
                new Prestamo(ana, "El Principito", diasAtraso: 5),
                new Prestamo(carlos, "Clean Architecture", diasAtraso: 3),
                new Prestamo(donaRosa, "Cien Anios de Soledad", diasAtraso: 30)
            };

            var moduloPrestamos = new ModuloPrestamos();
            foreach (var prestamo in prestamos)
            {
                moduloPrestamos.RegistrarDevolucion(prestamo);
            }

            var moduloReportes = new ModuloReportes();
            moduloReportes.GenerarReporteMultas(prestamos);

        }
    }
}
