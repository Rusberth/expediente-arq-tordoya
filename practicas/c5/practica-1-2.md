# Práctica 1 — herencia mentirosa (LSP)
 
**Dónde la encontré:** en mi primer intento (antes de pensar en LSP), pude haber puesto métodos como AjustarPrecio() y AnularVenta() directamente en la clase padre Usuario, razonando "total, todo usuario opera el sistema". Si hiciera eso, Cajero heredaría esos métodos por obligación, pero un cajero no puede ajustar precios ni anular ventas. Tendría que implementarlos lanzando una excepción (`NotSupportedException` o similar) solo para que el código compile. Esa es la herencia mentirosa: el hijo promete, por herencia, algo que no puede cumplir.
 
**Las pistas que la delatan, aplicadas a mi caso:**
- Acción prohibida: un Cajero.AjustarPrecio() que existe solo para lanzar una excepción.
- "Este caso es especial": cualquier comentario tipo `// Cajero no debería llamar esto` al lado de un método heredado.
**El arreglo — contrato mínimo en el padre + interfaz aparte para lo opcional:**
 
La buena noticia es que mi H1 ya evitó este error sin que lo hubiera hecho a propósito: `Usuario` (el padre) solo promete lo que CUALQUIER usuario puede cumplir sin excepción — `id`, `nombre`, `rol`, `iniciarSesion()`. Nada de `AjustarPrecio` ni `AnularVenta` ahí. Lo que antes hubiera sido "meterlo en el padre" ahora es una interfaz aparte, que solo firma quien realmente puede cumplirla.
 
<img width="1200" height="501" alt="LSP drawio" src="https://github.com/user-attachments/assets/fe6c9df6-e843-4bb7-9d3e-f4bd35f1cc27" />

 
---
 
# Práctica 2 — Los contratos de TUS roles (ISP)
 
**Tomando mi RF4** ("vendedor registra ventas, administrador ajusta stock y precios"), separo por capacidad en vez de por rol, y le pongo nombre de interfaz a cada capacidad:
 
| Interfaz (capacidad) | Método(s) | ¿Quién la firma? |
|---|---|---|
| IRegistradorDeVenta | RegistrarVenta() | Cajero |
| IConsultorDeStock | ConsultarStock() | Cajero **y** Encargado (la comparten) |
| IGestorDeCatalogo | RegistrarProducto(), AjustarPrecio() | Encargado |
| IAprobador | AprobarMerma() | Encargado |
| IAuditor | VerReportes() | Encargado |
 
**Qué comparten:** solo `IConsultorDeStock` ambos roles necesitan ver el stock disponible, ninguno necesita fingir el resto de las capacidades del otro.
 
**La regla aplicada:** si `Cajero` tuviera que firmar `IGestorDeCatalogo`, `IAprobador` o `IAuditor`, se vería obligado a fingir métodos que no le corresponden (exactamente el caso de la clase `IEmpleadoDeTienda` gorda, donde el cajero tenía que lanzar excepción en `AjustarPrecio` y `AnularVenta`). Por eso el contrato quedó partido en 5 capacidades chicas en vez de una grande.
 
<img width="1302" height="371" alt="ISP drawio" src="https://github.com/user-attachments/assets/242d9057-cdf0-4591-ba30-0f7dc55134f7" />

 
Encargado termina implementando las cuatro interfaces relevantes a su rol (acceso amplio del administrador), Cajero solo dos.
 
