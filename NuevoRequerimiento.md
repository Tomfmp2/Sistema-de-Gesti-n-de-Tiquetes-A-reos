 Selección de asientos y clases de vuelo

## Descripción

El sistema principal ya permite crear vuelos, reservas y emitir tiquetes. En este examen se agregará una nueva capacidad: **selección de asientos y manejo de clases de vuelo**. El objetivo es enriquecer el proceso de reserva para que no solo se aparten asientos por cantidad, sino también por **ubicación y categoría tarifaria**.

## Objetivos

- Incorporar clases de vuelo al sistema.
- Permitir selección de asientos individuales.
- Evitar asignación duplicada de asientos.
- Integrar la nueva lógica al flujo actual de reservas y tiquetes.

## Requerimiento a implementar

Agregar al proyecto principal lo siguiente:

- clase del asiento: económica, ejecutiva y primera clase;
- numeración de asientos por vuelo;
- selección manual de asiento durante la reserva;
- validación para impedir que un asiento ya reservado vuelva a asignarse;
- actualización de disponibilidad por clase;
- visualización en consola de asientos disponibles y ocupados.

# Lógica del negocio

## 1. Preparar la estructura del sistema

- Verificar que ya existan las entidades o tablas:
  - Vuelo
  - Reserva
  - Tiquete
  - Cliente
- Crear la entidad o tabla **ClaseVuelo**.
- Crear la entidad o tabla **Asiento**.
- Crear, si se necesita, una tabla intermedia o relación para asociar asientos a reservas o tiquetes.
- Confirmar que el vuelo permita manejar:
  - capacidad total
  - asientos disponibles
  - distribución por clase
  - estado del vuelo

## 2. Definir reglas del negocio

- Definir las clases de vuelo:
  - Económica
  - Ejecutiva
  - Primera clase
- Definir cuántos asientos tendrá cada clase por vuelo.
- Definir el formato de numeración de asientos, por ejemplo:
  - 1A, 1B, 1C
  - 10A, 10B, 10C
- Definir estados posibles del asiento:
  - Disponible
  - Reservado
  - Ocupado
  - Bloqueado, si aplica
- Definir si una reserva puede incluir:
  - un solo asiento
  - varios asientos
- Definir si el precio cambia según la clase.

## 3. Diseñar la entidad o tabla de asientos

- Crear campos como:
  - Id
  - VueloId
  - NumeroAsiento
  - ClaseVueloId
  - Estado
  - ReservaId, si aplica
  - TiqueteId, si aplica
- Relacionar cada asiento con un vuelo.
- Relacionar cada asiento con una clase.
- Garantizar que el número de asiento no se repita dentro del mismo vuelo.

## 4. Generar los asientos por vuelo

- Al crear un vuelo, generar automáticamente su mapa de asientos.
- Asignar la cantidad de asientos por clase.
- Crear la numeración de cada asiento.
- Guardar todos los asientos en MySQL.
- Confirmar que cada asiento quede inicialmente en estado **Disponible**.

## 5. Agregar opciones al menú de consola

- Crear opción **Ver asientos por vuelo**.
- Crear opción **Seleccionar asiento en reserva**.
- Crear opción **Consultar disponibilidad por clase**.
- Crear opción **Ver asientos ocupados**.
- Crear opción **Cambiar asiento**, si se desea extender la funcionalidad.

## 6. Mostrar asientos en consola

- Solicitar el identificador o código del vuelo.
- Validar que el vuelo exista.
- Consultar todos los asientos del vuelo.
- Agrupar los asientos por clase.
- Mostrar en consola:
  - número del asiento
  - clase
  - estado
- Diferenciar visualmente:
  - disponibles
  - reservados
  - ocupados

## 7. Flujo para selección de asiento durante la reserva

- Solicitar el identificador de la reserva o iniciar una nueva reserva.
- Verificar que la reserva exista o crearla.
- Solicitar el vuelo correspondiente.
- Mostrar las clases disponibles.
- Solicitar la clase deseada.
- Mostrar los asientos disponibles de esa clase.
- Solicitar el número del asiento elegido.
- Verificar que el asiento exista.
- Verificar que el asiento pertenezca al vuelo correcto.
- Verificar que el asiento pertenezca a la clase seleccionada.
- Verificar que el asiento esté en estado **Disponible**.

## 8. Validaciones antes de asignar el asiento

- No permitir seleccionar un asiento inexistente.
- No permitir seleccionar un asiento de otro vuelo.
- No permitir seleccionar un asiento ya reservado.
- No permitir seleccionar un asiento ya ocupado.
- No permitir duplicar el mismo asiento en otra reserva.
- Verificar que la cantidad de asientos seleccionados no exceda lo permitido por la reserva, si aplica.

## 9. Asignar el asiento a la reserva

- Cambiar el estado del asiento a **Reservado**.
- Relacionar el asiento con la reserva.
- Actualizar la disponibilidad de la clase correspondiente.
- Actualizar la disponibilidad general del vuelo, si aplica.
- Guardar los cambios en MySQL.
- Mostrar mensaje de éxito en consola.

## 10. Integrar la lógica con tiquetes

- Al emitir el tiquete, recuperar el asiento asignado.
- Mostrar el asiento en el tiquete.
- Cambiar el estado del asiento si el flujo lo requiere:
  - de Reservado a Ocupado
  - o mantenerlo según la lógica definida
- Garantizar consistencia entre reserva, asiento y tiquete.

## 11. Consultas que debe permitir el sistema

- Consultar asientos disponibles por vuelo.
- Consultar asientos disponibles por clase.
- Consultar asientos ocupados por vuelo.
- Consultar asientos asignados a una reserva.
- Consultar cantidad de asientos disponibles por clase.
- Consultar porcentaje de ocupación por clase.

