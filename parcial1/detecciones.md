# P1.1 — Detecciones (Parcial 1, Variante B — Ferretería "El Tornillo")
 
| Principio | Donde vive (clase y metodo) | Por que es una violacion |
|---|---|---|
| **S** -SRP | GestorDePedidos.ProcesarPedido | El metodo hace muchas cosas, calcula el descuento, guarda el pedido, imprime el comprobante y envia una notificacion.todas estan mezcladas en un solo metodo. |
| **O** -OCP | GestorDePedidos.ProcesarPedido (switch sobre tipoCliente) | Para agregar un nuevo tipo de cliente por ejemplo mayorista o algun cliente exclusivo hay que abrir este metodo y agregar un case nuevo, cada regla nueva obliga a tocar y volver a probar codigo que ya funcionaba. |
| **I** -ISP | IEmpleadoDeFerreteria, implementada por Vendedor | La interfaz obliga a Vendedor a implementar AutorizarVentaAlPorMayor, AjustarPrecio y VerReporteDeCompras, metodos que no le corresponden a un vendedor.  |
| **D** -DIP | GestorDePedidos.ProcesarPedido | El metodo crea directamente sus propias herramientas (new BaseDeDatosMySql(), new CorreoSmtp()) en lugar de recibirlas de afuera. Eso amarra o casa el pedido, si mañana cambian de base de datos o de forma de enviar el correo, hay que tocar esta clase. |
 
