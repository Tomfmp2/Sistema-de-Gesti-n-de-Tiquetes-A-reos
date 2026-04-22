# TODO — Sistema de gestión de tiquetes aéreos

## P0 — Bloqueantes (no compila / no corre)
- [ ] Arreglar errores de compilación en duplicados `Src/` (hoy revivieron por incluirse en build):
  - `Src/Modules/Baggage/UI/BaggageConsoleUI.cs`
  - `Src/Modules/FlightSeats/UI/FlightSeatConsoleUI.cs`
  - `Src/Modules/Reservations/Infrastructure/repository/ReservationRepository.cs`
- [ ] `dotnet run` debe compilar y abrir el menú principal.

## P1 — Limpieza de estructura (quedarnos solo con `src/`)
- [ ] **NO BORRAR `src/`**. (El intento previo de “quedarnos con una sola carpeta” causó pérdida de código/migraciones.)
- [ ] Si más adelante se hace limpieza de carpetas: primero inventario completo + build + backup/commit; solo después cualquier eliminación.

## P1 — UI / Menús / Formatos
- [ ] Migrar todos los módulos restantes a UI estándar:
  - Menú con flechas (Spectre) y fallback a consola plana.
  - CRUD en orden: Crear → Listar → Consultar → Actualizar → Eliminar → Volver.
  - Formularios con cancelación (`0`, `c`, `cancelar`) y validación de entrada.
  - Listados con tablas y vistas tipo “ficha” (Campo/Valor).
- [ ] Revisar módulos que aún no aparecen en el menú principal y agregarlos por rol.

## P1 — Login (rendimiento y estabilidad)
- [ ] Ajustar pre-check de DB para que no lance stacktrace (timeout tratado como transitorio).
- [ ] Revisar cadena `MYSQL_CONNECTION`/SSL/pooling si la DB sigue “cortándose”.
- [ ] Considerar reducir número de queries en login (ideal: traer role + client en una sola consulta).
- [ ] Permitir crear un usuario desde la pantalla de login (opción "Registrarse") con wizard amigable.
- [ ] Garantizar un usuario **ROOT** default (username `ROOT`, contraseña `12345`) con todos los permisos/rol administrador:
  - Seed idempotente para que funcione en otros computadores/DBs.
  - Asegurar que existe `system_roles` con rol admin y que ROOT lo use.

## P1 — Registro de usuario (requisitos nuevos)
- [ ] Al registrar usuario pedir:
  - Email (y preguntar si tiene más de uno).
  - Número telefónico (y preguntar si tiene más de uno).
- [ ] Persistir emails/teléfonos en tablas correspondientes (`person_emails`, `person_phones`) dentro de la misma transacción del registro.
- [ ] **Regla de personas/documento**:
  - El par **(document_type_id, document_number)** debe identificar **una única persona** (documento único por persona).
  - Se debe permitir que **varios usuarios** apunten a la **misma persona** (mismo documento), sin duplicar `persons`.

## P1 — Reservas (requisitos nuevos)
- [ ] Después de elegir el vuelo, pedir **dirección de facturación**.
- [ ] Persistir la dirección y relacionarla con reserva/pago/factura según el modelo actual.
- [ ] Vista **Cliente**: permitir **crear reservación** (no solo listar).
- [ ] Vista **Cliente**: permitir **hacer Check-in**.
- [ ] Sección **Check-in**: mostrar si el cliente tiene **reservas disponibles** para hacer check-in (si no, mostrar mensaje claro).

## P2 — Rúbrica pendiente (módulos/funcionalidades)
- [ ] UI completa de Vuelos (crear/listar/actualizar, capacidad y estado).
- [ ] UI completa de Clientes (registro, consulta, actualización).
- [ ] Emisión de tiquetes solo desde reservas confirmadas (UI + reglas).
- [ ] Pagos: UI + estados simulados (pendiente/pagado/rechazado) y relación con reserva/tiquete.
- [ ] Reportes LINQ: implementar menú de reportes (ocupación, disponibles, top clientes, destinos, ingresos, etc.).

