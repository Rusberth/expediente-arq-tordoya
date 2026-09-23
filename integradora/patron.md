# Parte 3: El patron: Strategy para la tarifa por tipo de vehiculo

## Cual es el requerimiento?

*"El sistema registra ESTADIAS: cada vehiculo (auto, moto o residente) entra, permanece y sale pagando por hora segun su tipo."*

Son tres formas de calcular el mismo número (el costo por hora), que cambian segun el tipo de vehículo  y ese catalogo de tipos puede crecer, nada impide que el edificio sume mañana "visitante" o "vehiculo de carga".

## Qué patron aplico?

**Strategy.**

## El diseño

```csharp
public interface IEstrategiaDeTarifa
{
    decimal CalcularTarifaPorHora();
}

public class TarifaAuto : IEstrategiaDeTarifa
{
    public decimal CalcularTarifaPorHora() => 5m;
}

public class TarifaMoto : IEstrategiaDeTarifa
{
    public decimal CalcularTarifaPorHora() => 3m;
}

public class TarifaResidente : IEstrategiaDeTarifa
{
    public decimal CalcularTarifaPorHora() => 1m;
}
```

GestorDeEstadias.RegistrarSalida() recibe la estrategia ya elegida por parámetro, y ya no pregunta de que tipo es el vehiculo, solo le pide el calculo a quien le pasaron.

## Por qué Strategy y no otro patron?

No es un problema de **creacion** de objetos, eso seria Factory, y aqui nadie tiene que decidir "qué instancia construir" a partir de un dato en tiempo de ejecucion antes de que el objeto exista. Lo que cambia es el **calculo** que se aplica sobre una Estadia que ya existe, eso es comportamiento, no creacion.

Tampoco es Decorator, las tarifas no se combinan ni se apilan. Un vehiculo es auto, O moto, O residente nunca una mezcla de las tres. Decorator resuelve agregados combinables; aqui no hay nada para combinar.

Y no es Adapter: no hay ningun sistema externo cuyo formato haya que traducir.

Strategy es el que encaja exacto: un contrato comun (IEstrategiaDeTarifa), una clase por variante, y quien lo usa recibe la variante ya elegida sin saber cual es.

## Qué pasa sin el

Vuelve el switch dentro de GestorDeEstadias.RegistrarSalida(), la violación de OCP que identifique en detecciones.md. Cada tipo de vehiculo nuevo obliga a reabrir esa clase y agregar un case mas, con el riesgo de romper los tipos que ya funcionaban.
