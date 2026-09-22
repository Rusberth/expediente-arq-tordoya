# Parte 2: La cirugia SOLID: las 3 violaciones

Sobre esqueleto-B.cs, especificamente dentro de GestorDeEstadias.RegistrarSalida().

## 1. SRP (Single Responsibility)

**Donde?:** todo el metodo GestorDeEstadias.RegistrarSalida().

**Por que?:** mezcla cuatro responsabilidades distintas en un mismo metodo, calcula la tarifa, guarda la estadia en la base de datos, imprime el ticket por consola, y envia el aviso por WhatsApp. Cualquier cambio en cualquiera de esas cuatro cosas (una regla de tarifa, el motor de base de datos, el formato del ticket, el proveedor de mensajeria) obliga a tocar la misma clase.

## 2. OCP (Open/Closed)

**Donde?:** el switch (tipoVehiculo) dentro de RegistrarSalida().

**Por que?:** la tarifa por hora esta resuelta con un switch sobre el tipo de vehículo. Cada tipo nuevo (por ejemplo "visitante" o "vehiculo de carga") obliga a reabrir este metodo y agregar un case mas, arriesgando romper los tipos que ya funcionaban.

## 3. DIP (Dependency Inversion)

**Donde:?** new BaseDeDatosParqueo() y new WhatsAppDelEdificio(), instanciados directamente dentro de RegistrarSalida().

**Por que?:** GestorDeEstadias queda atado a dos implementaciones concretas de bajo nivel. No se puede cambiar de proveedor de mensajería, ni probar el registro de salida sin una base de datos real, sin modificar esta clase.

---

(LSP e ISP no aplican sobre este archivo: no hay herencia ni interfaces todavía en esqueleto-B.cs.

## La violacion curada: OCP: Ver refactor.cs

Se eligió curar **OCP** porque es la violacion que, sin corregir, mas crece con el tiempo: cada tipo de vehiculo nuevo que agregue el edificio vuelve a tocar GestorDeEstadias.

