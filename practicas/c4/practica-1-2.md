
## Práctica 1 — Encontrar la clase gorda (SRP)

La clase gorda que identifico: si no aplicara SRP, Venta terminaría absorbiendo todo lo que pasa al confirmar una venta, el cálculo del total (negocio), el guardado en base de datos (guardado), la impresión del ticket (pantalla) y el aviso de stock bajo.


<img width="1201" height="331" alt="SRP drawio" src="https://github.com/user-attachments/assets/e76b60a3-bf5d-4f98-b2f0-ff8338a65c93" />


## Test — ¿qué departamento pediría cambios en cada caja?
 
| Caja | Responsabilidad | Departamento que pediría el cambio |
|---|---|---|
| CalculadoraDeTotalVenta | Negocio (cómo se calcula el total) | Contabilidad |
| RepositorioDeVenta | Guardado (dónde y cómo se persiste) | Sistemas / TI |
| ImpresoraDeTicket | Pantalla (formato del comprobante) | Atención al cliente / el dueño |
| NotificadorDeStockBajo | Aviso (canal de notificación) | Encargado / marketing |
 
Cuatro departamentos distintos, cuatro razones de cambio distintas, cuatro cajas distintas. Si estuviera todo junto en Venta, un pedido de contabilidad podría romper el ticket sin que nadie lo haya tocado a propósito.



# Práctica 2 — Tu dominio esconde un switch (OCP)
 
**Dónde lo encontré:** en mis tipos de entidad. MovimientoDeStock tiene un atributo tipo (venta, ingreso, devolución, merma, ajuste) que, si se resuelve con un switch dentro de un método Aplicar(), obliga a abrir esa clase cada vez que aparece una causa nueva de movimiento de stock.
 
 
## La interfaz que lo cierra
 
<img width="1342" height="331" alt="OCP drawio" src="https://github.com/user-attachments/assets/5115b589-e37e-4a7a-bc8c-879e3fcf4ed9" />

 
**Nombre de la interfaz:** ITipoDeMovimiento

**Método:** Aplicar(stockActual, cantidad) : int

**Implementaciones:** MovimientoPorVenta, MovimientoPorIngreso, MovimientoPorDevolucion, MovimientoPorMerma.
 
## La pregunta clave
 
Cuando el negocio pida un tipo nuevo, por ejemplo, "transferencia entre sucursales" el día que abra un segundo local:
 
- **Qué clase NACE:** MovimientoPorTransferencia, una clase nueva que implementa ITipoDeMovimiento.
- **Qué clase NO se toca:** MovimientoDeStock (la que orquesta) y las cuatro implementaciones existentes (MovimientoPorVenta, MovimientoPorIngreso, MovimientoPorDevolucion, MovimientoPorMerma) ninguna se abre ni se recompila para que la transferencia funcione.
