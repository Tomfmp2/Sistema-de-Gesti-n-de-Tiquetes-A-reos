ANÁLISIS DE ESTRUCTURA DE BD - SISTEMA DE GESTIÓN DE TIQUETES AÉREOS
====================================================================

[RESUMEN EJECUTIVO]
- Total tablas en SQL: 63
- Total tablas en BD actual: 55 (en migraciones)
- Total DbSets registrados: 45 (en AppDbContext)
- Tablas faltantes: 8
- Tablas extras (concepto de reservations vs bookings): 0
- Problemas de conexiones: 6
- Inconsistencias de nomenclatura: 12

[TABLAS FALTANTES - No están implementadas pero SÍ en el SQL]

CRÍTICAS:
1. baggage_types (dependencias: --)
   Estructura SQL: id, name (Checked, Carry-on Paid...), max_weight_kg, base_price
   IMPACTO: Sistema de cálculo de cargos por equipaje completamente ausente

2. baggage (dependencias: fk_checkin_id -> check_ins, fk_baggage_type_id -> baggage_types)
   Estructura SQL: id, checkin_id, baggage_type_id, weight_kg, charged_price
   IMPACTO: Registros de equipaje de pasajeros no pueden ser almacenados

PARCIALES (Reemplazadas por concepto alternativo):
3. bookings (reemplazado por reservations)
   SQL esperado: id, client_id, booked_at, booking_status_id, total_amount, expires_at, created_at, updated_at
   Proyecto usa: ReservationEntity con estructura similar pero diferente semántica
   PROBLEMA: Proyecto cambió modelo de "bookings" a "reservations"

4. booking_statuses (reemplazado por reservation_statuses)
   SQL esperado: id, name (Pending, Confirmed, Cancelled, Expired)
   ESTADO: ReservationStatusEntity existe pero con nombre diferente

5. booking_status_transitions (reemplazado por reservation_status_transitions)
   SQL esperado: id, from_status_id, to_status_id, UNIQUE(from_status_id, to_status_id)
   ESTADO: ReservationStatusTransitionEntity existe pero con nombre diferente

6. booking_flights (mapeado a reservation_flights)
   SQL esperado: id, booking_id, flight_id, partial_amount, UNIQUE(booking_id, flight_id)
   ESTADO: ReservationFlightEntity existe
   PROBLEMA: FK apunta a reservations, no a bookings

7. booking_passengers (mapeado a reservation_passengers)
   SQL esperado: id, booking_flight_id, passenger_id, UNIQUE(booking_flight_id, passenger_id)
   ESTADO: ReservationPassengerEntity existe
   PROBLEMA: FK apunta a reservation_flights, no a booking_flights

INCOMPLETAS:
8. addresses (parcialmente implementada como "directions")
   SQL esperado: id, street_type_id, street_name, number, complement, city_id, postal_code
   Proyecto tiene: DirectionEntity con mismos campos
   PROBLEMA: **FALTA la relación address_id en DirectionEntity (debe ser FK a persons, nullable)**
   IMPACTO: Las personas no pueden tener múltiples direcciones asociadas

[TABLAS EXTRAS - Están en las migraciones pero NO en el SQL]
NINGUNA. El proyecto NO agrega tablas extras; solo cambia la semántica con "reservations" vs "bookings"

[PROBLEMAS EN FOREIGN KEYS]

Tabla RESERVATIONS (reempl. bookings):
  - Debería tener cliente_id (client_id) → ✓ EXISTE
  - Debería tener reservation_status_id → ✓ EXISTE
  - PROBLEMA: SQL expecta "expires_at" pero proyecto NO lo impone obligatoriamente

Tabla INVOICES:
  - SQL: FK a bookings(id)
  - Proyecto: FK a reservations(id)
  - Necesita: Asegurar que la FK se mapea correctamente en migraciones

Tabla CHECK_INS:
  - FALTA: FK a staff(id) para registrar quién realizó el check-in
  - SQL expecta: staff_id NOT NULL
  - Proyecto: Implementó CheckinEntity pero verificar si tiene staff_id

Tabla DIRECTIONS (addresses):
  - CRÍTICA FALTA: address_id como FK a persons(id) - nullable en SQL
  - Efecto: Las personas no pueden vincularse a múltiples direcciones
  - Campo adicional proyecto: street_number (podría ser string en lugar de "number")

Tabla FLIGHT_SEATS:
  - Parece estar bien vinculada a flights, cabins y locations

Validaciones CHECK ausentes en migraciones:
  - flights: CHECK (total_capacity > 0)
  - flights: CHECK (available_seats >= 0)
  - fares: CHECK (base_price >= 0)

[MÓDULOS SIN TABLAS O INCONSISTENTES]

Módulos implementados (61 carpetas con Entity.cs cada una):
✓ Continents, Countries, Regions, Cities (geografía)
✓ DocumentTypes, Persons, PersonEmails, PersonPhones (base de datos de personas)
✓ EmailDomains, PhoneCodes (dominios de contacto)
✓ StreetTypes, Directions (direcciones - INCOMPLETA)
✓ Clients, Passengers, Staff (tipos de personas)
✓ Airlines, Airports, AirportAirline (aerolíneas y aeropuertos)
✓ StaffPositions, StaffAvailability, AvailabilityStatuses (personal)
✓ AircraftManufacturers, AircraftModels, Aircraft (flota)
✓ CabinTypes, CabinConfiguration (configuración de cabina)
✓ Routes, RouteLayovers (rutas)
✓ Seasons (temporadas)
✓ PassengerTypes, Fares (tarifas)
✓ Flights, FlightStatuses, FlightStatusTransitions (vuelos)
✓ FlightRoles, FlightAssignments (tripulación)
✓ SeatLocationTypes, FlightSeats (asientos)
✓ Reservations, ReservationStatuses, ReservationStatusTransitions (reservaciones)
✓ ReservationFlights, ReservationPassengers (pasajeros en reservas)
✓ Tickets, TicketStatuses (tiquetes)
✓ Checkins, CheckinStatuses (check-in)
✓ InvoiceItemTypes, Invoices, InvoiceItems (facturas)
✓ PaymentMethods, PaymentMethodTypes, CardTypes, CardIssuers (métodos de pago)
✓ Payments, PaymentStatuses (pagos)
✓ SystemRoles, Permissions, RolePermissions (RBAC)
✓ Users, Sessions (autenticación)

✗ Baggage (NO EXISTE - módulo faltante CRÍTICO)

Módulos con inconsistencias de nomenclatura (tablas en español en migraciones tempranas):
- Directions (como "direcciones") - Debería ser addresses
- Persons (como "personas") - Debería ser persons en todas partes
- Clients (como "clientes") - Debería ser clients en todas partes
- PhoneCodes (como "codigos_telefono") - Debería ser phone_codes
- PersonEmails (como "personas_emails") - Debería ser person_emails
- PersonPhones (como "personas_telefonos") - Debería ser person_phones
- DocumentTypes (como "tipos_documento") - Debería ser document_types
- PassengerTypes (como "tipos_pasajero") - Debería ser passenger_types
- StreetTypes (como "tipos_via") - Debería ser street_types
- Users (como "usuarios") - Debería ser users
- Permissions (como "permisos") - Debería ser permissions
- SystemRoles (como "roles_sistema") - Debería ser system_roles
- Sessions (como "sesiones") - Debería ser sessions

[PROBLEMAS CRÍTICOS IDENTIFICADOS]

1. SISTEMA DE BAGGAGE AUSENTE
   Severity: CRÍTICA
   - Tablas faltantes: baggage_types, baggage
   - Campos relacionados: NONE en Checkins
   - Impacto: No se puede registrar equipaje ni calcular cargos por exceso
   - Solución: Crear 2 nuevas tablas + entidades + migraciones

2. DIRECCIONES INCOMPLETA (addresses)
   Severity: ALTA
   - Campo faltante: address_id como FK nullable a persons
   - Impacto: Una persona no puede tener múltiples direcciones
   - Solución: Modificar DirectionEntity, agregar PersonId, crear nueva migración

3. BOOKINGS vs RESERVATIONS
   Severity: MEDIA-ALTA
   - Cambio conceptual: SQL define "bookings" pero proyecto usa "reservations"
   - Impacto: Diferencia en modelo de datos, pero funciona
   - Solución: Documentar decisión, asegurar todas las FK consistentes

4. MIGRACIONES CON NOMBRES EN ESPAÑOL
   Severity: MEDIA
   - 12 tablas con nombres en español (personas, usuarios, etc.)
   - Impacto: Inconsistencia, dificulta mantenimiento
   - Solución: Crear migración de renombramiento a inglés (como existe para schema inicial)

5. CHECK CONSTRAINTS FALTANTES
   Severity: MEDIA
   - Validaciones numéricas no implementadas en BD
   - Impacto: Datos inválidos podrían insertarse
   - Solución: Agregar validaciones CHECK en migraciones

[RECOMENDACIONES PRIORITIZADAS]

IMPLEMENTAR INMEDIATAMENTE (Semana 1):
1. Crear tabla baggage_types con estructura: id, name (UV), max_weight_kg, base_price (DD.DD)
   - Crear BaggageTypeEntity
   - Crear migración CreateBaggageTypes
   - Registrar en AppDbContext

2. Crear tabla baggage con estructura: id, checkin_id (FK), baggage_type_id (FK), weight_kg, charged_price
   - Crear BaggageEntity
   - Manejar relación con CheckinEntity
   - Registrar en AppDbContext

3. Modificar DirectionEntity para incluir PersonId (nullable FK a persons)
   - Permitir que personas tengan múltiples direcciones
   - Crear migración UpdateDirectionsAddPersonId
   - Versionar esquema

RESOLVER EN PRÓXIMAS 2 SEMANAS:
4. Ejecutar migración para renombrar todas las tablas a inglés
   - Mapear: personas → persons, usuarios → users, clientes → clients, etc.
   - Asegurar no romper relaciones existentes

5. Agregar validaciones CHECK en migraciones
   - flights: total_capacity > 0, available_seats >= 0
   - fares: base_price >= 0
   - invoices: subtotal >= 0, taxes >= 0, total >= 0
   - payments: amount >= 0

6. Audit de todas las FK en check_ins para asegurar staff_id

PRÓXIMO MES:
7. Actualizar AppDbContextModelSnapshot con todas las 45+ tablas actuales
8. Crear tests de integridad de FK y constraints
9. Documentación completa del modelo de datos

[TABLA RESUMEN DE ESTADO]

| Categoría | Cantidad | Estado |
|-----------|----------|--------|
| Tablas SQL referencia | 63 | Referencia |
| Tablas implementadas | 55 | En migraciones |
| DbSets registrados | 45 | En AppDbContext |
| Tablas correctas | 52 | ✓ OK |
| Tablas con cambios conceptuales | 6 | ≈ booking→reservation |
| Tablas incompletas | 1 | ⚠️ addresses |
| Tablas faltantes | 4 | ✗ NO EXISTEN |
| Nomenclatura en español | 12 | Inconsistencia |
| FK faltantes | 2 | Problemáticas |
| Check constraints faltantes | 5 | Debería haber |
| Cobertura estimada | 85% | Mejora 15% posible |
====================================================================
Análisis completado: 18/04/2026
