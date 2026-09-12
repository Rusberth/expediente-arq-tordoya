# Singleton en "El Ahorro" — justificación

## El test del arquitecto: ¿el mundo real permite DOS?

**Mi candidato: GeneradorDeCorrelativoDeVenta.** El minimarket tiene dos cajas físicas, pero la numeración de boletas tiene que ser **una sola secuencia para todo el negocio**, si cada caja llevara su propio contador independiente, ambas emitirían tarde o temprano una "Boleta #1", y dos boletas con el mismo número rompen la trazabilidad de las ventas. Esto conecta directo con el atributo de calidad que elegí en H1: el sistema tiene que poder responder sin ambigüedad "qué se vendió y en qué orden", y una numeración duplicada es exactamente el tipo de descuadre que ese atributo busca evitar. El mundo real de un minimarket **no permite dos numeraciones paralelas de boletas**  por eso el candado (constructor privado) y la puerta única (Instancia) tienen sentido acá.

## El candidato que descarté, y por qué

Podría pensarse en CajaRegistradora como Singleton. Pero en mi caso **no aplica**: el negocio tiene explícitamente **dos cajas físicas** operando a la vez, con cuatro cajeros turnándose. Forzar CajaRegistradora a una sola instancia sería un antipatrón, tratar de volver único algo que el negocio real permite (y necesita) que sea múltiple.

