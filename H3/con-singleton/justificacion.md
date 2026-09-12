# Singleton en "El Ahorro" — justificación

## El test del arquitecto: ¿el mundo real permite DOS?

**Mi candidato: `GeneradorDeCorrelativoDeVenta`.** El minimarket tiene dos cajas físicas, pero la numeración de boletas tiene que ser **una sola secuencia para todo el negocio** — si cada caja llevara su propio contador independiente, ambas emitirían tarde o temprano una "Boleta #1", y dos boletas con el mismo número rompen la trazabilidad de las ventas. Esto conecta directo con el atributo de calidad que elegí en H1 (**idoneidad funcional**): el sistema tiene que poder responder sin ambigüedad "qué se vendió y en qué orden", y una numeración duplicada es exactamente el tipo de descuadre que ese atributo busca evitar. El mundo real de un minimarket **no permite dos numeraciones paralelas de boletas** — por eso el candado (constructor privado) y la puerta única (`Instancia`) tienen sentido acá.

## El candidato que descarté, y por qué

Podría pensarse en `CajaRegistradora` como Singleton ("la caja" era uno de los ejemplos que dio el profesor). Pero en mi caso **no aplica**: el negocio tiene explícitamente **dos cajas físicas** operando a la vez, con cuatro cajeros turnándose. Forzar `CajaRegistradora` a una sola instancia sería el antipatrón exacto que se advirtió en clase — tratar de volver único algo que el negocio real permite (y necesita) que sea múltiple, como el ejemplo del "lapicero oficial de la escuela".

## Resumen para la defensa

| | ¿Permite el mundo real que haya dos? | ¿Es Singleton en mi caso? |
|---|---|---|
| Numeración de boletas | No — rompería la trazabilidad de ventas | Sí, `GeneradorDeCorrelativoDeVenta` |
| Caja registradora | Sí — el negocio tiene dos cajas reales | No — sería el antipatrón |
