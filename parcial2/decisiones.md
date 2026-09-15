# Parcial 2 · Arquitectura de Software · Variante A (Biblioteca Municipal)

## P2.1 — Elegir y justificar

### Situacion 1 — Avisos de vencimiento de prestamos

**Patron: Observer.**

Hoy el modulo de prestamos llama uno por uno a cada interesado (correo, morosidad,
recepcion) y pronto tambien a multas municipales, con la promesa de "quien sabe que
mas" despues. Eso significa que cada interesado nuevo obliga a abrir y modificar el
modulo de prestamos, que ya funciona y ya esta probado. Con Observer, el modulo de
prestamos (el sujeto) solo mantiene una lista de suscriptores que cumplen un contrato
comun (por ejemplo ISuscriptorVencimiento.Notificar(...)) y los recorre al vencer un
prestamo; agregar multas municipales o cualquier otro interesado futuro es agregar una
clase nueva, sin tocar el modulo de prestamos. No es Strategy porque aqui no se elige
una forma de hacer algo entre varias alternativas: se debe avisar a todos los
interesados a la vez. Tampoco es Adapter porque no hay una interfaz externa incompatible
que traducir, sino una lista de destinatarios que crece. Si no se aplica Observer, cada
interesado nuevo cuesta una modificacion y una re-prueba del modulo de prestamos, con
riesgo de romper las notificaciones que ya funcionan.

### Situacion 2 — Calculo de multas por atraso

**Patron: Strategy.**

La multa depende del tipo de socio (infantil, adulto, tercera edad) y las reglas cambian
cada año por decision del concejo; hoy esa logica vive en un if/else duplicado en el
modulo de prestamos y copiado en el modulo de reportes. Strategy encierra cada regla de
calculo detras de un contrato comun (ICalculadoraMulta), de modo que prestamos y
reportes piden la calculadora correspondiente en vez de repetir el if/else, y un cambio
de regla (o un tipo de socio nuevo) es una clase que se agrega o se modifica, no dos
lugares que hay que recordar sincronizar. No es Observer porque no hay una lista de
interesados a notificar, sino una unica decision de "como calcular" que varia segun el
caso. No es Adapter porque no existe una interfaz externa incompatible: el problema es
logica interna duplicada, no una traduccion hacia un sistema ajeno. Si no se aplica
Strategy, cada cambio de reglas obliga a editar dos archivos a la vez, y es facil
actualizar uno y olvidar el otro, dejando multas inconsistentes entre prestamos y
reportes.

### Situacion 3 — Integracion con el Sistema Estatal de Bibliotecas

**Patron: Adapter.**

El servicio externo es fijo y no se puede modificar: expone PushRecord(jsonPayload,
isoDate, originCode) en ingles, con fechas en otro formato y codigos que el dominio de
la biblioteca no usa, y ademas cambia de version periodicamente. Un Adapter define el
contrato propio del dominio (por ejemplo IRegistradorCentral.RegistrarLibro(Libro
libro)) y, dentro de una sola clase, traduce esos datos al jsonPayload, isoDate y
originCode que pide el servicio estatal. No es Strategy porque no hay varias formas
intercambiables de resolver lo mismo: hay un unico sistema externo con el que hablar, mal
alineado con el dominio propio. No es Observer porque el problema no es avisar a varios
interesados de un evento, sino hacer compatibles dos interfaces distintas. Si no se
aplica Adapter, cada cambio de version del servicio estatal (nombres, formato de fecha,
codigos) obliga a tocar el codigo del catalogo en todos los puntos donde se llama
directamente a PushRecord, en vez de tocar solo la clase adaptadora.

## P2.3 — La conexion SOLID

La implementacion de P2.2 (Strategy para el calculo de multas) rescata el **Principio de
Abierto/Cerrado (OCP)**. Se ve concretamente en ICalculadoraMulta y en
RegistroCalculadorasMulta: ModuloPrestamos.RegistrarDevolucion y
ModuloReportes.GenerarReporteMultas solo conocen el contrato ICalculadoraMulta y
piden la calculadora al registro, nunca hacen if. Si el concejo
agrega un cuarto tipo de socio, la extension es una clase nueva (MultaEstudiante :
ICalculadoraMulta) mas una linea en RegistroCalculadorasMulta; ninguna linea dentro de
ModuloPrestamos ni de ModuloReportes se modifica. Eso es exactamente OCP: el sistema
queda abierto a nuevas reglas de multa, pero cerrado a modificaciones en el codigo que ya
funciona y ya esta probado.
