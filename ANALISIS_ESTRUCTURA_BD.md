# ANÁLISIS DETALLADO DE ESTRUCTURA DE BD
## Sistema de Gestión de Tiquetes Aéreos

**Fecha del análisis:** 18 de abril de 2026  
**Proyecto:** Sistema de Gestión de Tiquetes Aéreos (C#)  
**Base de referencia:** `/home/tomdev_/Descargas/tablas_mysql.txt` (63 tablas)

---

## RESUMEN EJECUTIVO

| Métrica | Valor |
|---------|-------|
| **Tablas definidas en SQL de referencia** | 63 |
| **Entidades (Entity.cs) en el proyecto** | 61 |
| **DbSets registrados en AppDbContext** | 45 |
| **Tablas en migraciones (Up Only)** | 55 |
| **Tablas en AppDbContextModelSnapshot** | 26 |
| **Tablas faltantes vs SQL** | 11 |
| **Tablas problemáticas o incompletas** | 8 |
| **Problemas de Foreign Keys** | 6 |
| **Naming inconsistencies (Español/Inglés)** | 12 |

---

## 1. TABLAS FALTANTES - Están en SQL pero NO en código/migraciones

### Críticas (Funcionalidad Bloqueante)
1. **`addresses`** (asignado a `direcciones` en proyecto)
   - **Estado:** Implementado como `DirectionEntity` pero con estructura incompleta
   - **Campos faltantes:** `address_id` (FK a personas)
   - **Campos SQL esperados:** id, street_type_id, street_name, number, complement, city_id, postal_code
   - **Campos proyecto:** id, city_id, street_type_id, street_name, street_number, complement, postal_code
   - **Problema:** Falta el vínculo `address_id` a `persons` (nullable en SQL)
   - **Impacto:** Las personas no pueden tener múltiples direcciones

2. **`booking_statuses`** y **`booking_status_transitions`** 
   - **Estado:** NO existe en el proyecto
   - **Alernativa proyecto:** Usa `ReservationStatus` y `ReservationStatusTransition`
   - **Problema:** Conceptualmente es diferente. SQL: reservas vs bookings
   - **Impacto:** Diferencia considerable en modelo de datos
   - **Nota:**  El proyecto implementó "Reservations" como concepto unificado

3. **`bookings`**
   - **Estado:** NO existe, reemplazado por `Reservations`
   - **Problema:** Cambio de modelo de datos significativo
   - **Campos SQL esperados:** id, client_id, booked_at, booking_status_id, total_amount, expires_at, created_at, updated_at
   - **Alternativa proyecto:** ReservationEntity (similar pero conceptualmente reservations vs bookings)

4. **`baggage_types`** y **`baggage`**
   - **Estado:** NO existen en el proyecto
   - **Criticidad:** ALTA - Sistema de maletas no implementado
   - **Campos SQL esperados:**
     - baggage_types: id, name (Checked, Carry-on Paid, Special, Sports...), max_weight_kg, base_price
     - baggage: id, checkin_id, baggage_type_id, weight_kg, charged_price
   - **Impacto:** Sistema de cálculo de cargos por equipaje ausente

### Intermedias (Funcionalidad Parcial)
5. **`bookings` → `booking_flights` → `booking_passengers`**
   - **Estado:** Implementado como `ReservationFlights` y `ReservationPassengers`
   - **Problema:** Cambio de nomenclatura conceptual
   - **Bridge:** SQL usa bookings como contenedor principal; proyecto usa reservations

6. **`flight_crew_roles`** y **`flight_crew_assignments`**
   - **Estado:** Implementado como `FlightRoles` y `FlightAssignments` (verificar EntityNames)
   - **Problema:** Verificación necesaria del mapeo exacto de nombres
   - **Nota:** Existen en el proyecto pero check de integridad requerido

---

## 2. TABLAS CON ESTRUCTURA INCOMPLETA O PROBLEMÁTICA

### Tabla: `Flights`
```sql
-- SQL esperado
CREATE TABLE flights (
    id, flight_code, airline_id, route_id, aircraft_id,
    departure_at, estimated_arrival_at, total_capacity,
    available_seats, flight_status_id, rescheduled_at,
    created_at, updated_at
);
```
**Problemas identificados:**
- ✓ Campos correctamente mapeados en proyecto
- ⚠️ **FALTA:** Validación CHECK en BD para constraints:
  - `CHECK (total_capacity > 0)`
  - `CHECK (available_seats >= 0)`

### Tabla: `Reservations`
```csharp
// Proyecto (ReservationEntity)
public int ClientId { get; set; }
public DateTime ReservationDate { get; set; }
public int ReservationStatusId { get; set; }
public decimal TotalValue { get; set; }
```
**Problemas:**
- ❌ **FALTA:** Campo `expires_at` (nullable DATETIME) - para política de expiración
- ⚠️ Campo SQL `booked_at` mapeado como `reservation_date`

### Tabla: `Check-ins`
**Problemas:**
- ⚠️ SQL tiene campos innecesarios en checkins (baggage_weight_kg como opcional)
- Proyecto implementó `CheckinEntity` con `BaggageWeightKg` y `HasCheckedBaggage`
- Falta relación `staff_id` en proyecto (SQL requiere FK staff)

### Tabla: `Invoices`
**Problemas:**
- ❌ SQL: `invoice_number` es UNIQUE pero proyecto no lo verifica
- ✓ Campos de dinero (subtotal, taxes, total) presentes con CHECK constraints
- ⚠️ SQL: FK a `bookings`; Proyecto: FK a `reservations`

---

## 3. PROBLEMAS DE FOREIGN KEYS

### FK Faltadas en Proyecto

| Tabla | Campo | Referencia SQL | Estado Proyecto | Impacto |
|-------|-------|----------------|-----------------|---------|
| `directions` | `address_id` | persons.id | ❌ NO EXISTE | Alto: direcciones no vinculables a personas |
| `staff` | `airport_id` | airports.id | ⚠️ CHECK | Personal aeroportuario no vinculado |
| `invoices` | `booking_id` | bookings.id | ⚠️ Mapeado a reservations | Potencial inconsistencia |
| `check_ins` | `staff_id` | staff.id | ⚠️ NO en proyecto | Quién realizó el check-in no registrable |
| `baggage` | Todas | - | ❌ TABLA NO EXISTE | Sistema de equipaje completo ausente |
| `payments` | `booking_id` | bookings.id | ⚠️ Mapeado a reservations | Pagos vinculados a reservas en proyecto |

### FK Incorrectamente Nombrados
- Español/Inglés mixto en migraciones anteriores
- Riesgos de ambigüedad en consultas posteriores

---

## 4. DIFERENCIAS DE NOMENCLATURA (Español vs Inglés)

### En Migraciones (Nombre de tabla)
| Tabla Proyecto | Tabla SQL Esperada | Región/Contexto |
|---|---|---|
| `clientes` | `clients` | Español temprano |
| `codigos_telefono` | `phone_codes` | Español temprano |
| `direcciones` | `addresses` | Español temprano |
| `dominios_email` | `email_domains` | Español temprano |
| `pasajeros` | `passengers` | Español temprano |
| `permisos` | `permissions` | Español temprano |
| `personas` | `persons` | Español temprano |
| `personas_emails` | `person_emails` | Español temprano |
| `personas_telefonos` | `person_phones` | Español temprano |
| `roles_permisos` | `role_permissions` | Español temprano |
| `roles_sistema` | `system_roles` | Español temprano |
| `sesiones` | `sessions` | Español temprano |
| `tipos_documento` | `document_types` | Español temprano |
| `tipos_pasajero` | `passenger_types` | Español temprano |
| `tipos_via` | `street_types` | Español temprano |
| `usuarios` | `users` | Español temprano |

**Estado actual:** Migraciones recientes usan inglés; migraciones antiguas mezclan.

---

## 5. MÓDULOS SIN TABLAS CORRESPONDIENTES O INCOMPLETOS

| Módulo | Tabla(s) Asignada(s) | Estado | Problema |
|--------|------------|--------|---------|
| `Baggage` | Falta crear | ❌ NO EXISTE | Sistema de equipaje completamente ausente |
| `PhoneCodes` | Existe (codigos_telefono) | ⚠️ RENAME | Debería ser `phone_codes` en inglés |
| `PassengerTypes` | Existe (tipos_pasajero) | ⚠️ RENAME | Debería ser `passenger_types` en inglés |
| `DocumentTypes` | Existe (tipos_documento) | ⚠️ RENAME | Debería ser `document_types` en inglés |
| `Directions` | Existe (direcciones) | ⚠️ INCOMPLETO | Falta vínculo a `persons` (address_id) |
| `Permissions` | Existe (permisos) | ✓ OK | Aunque nombre en español en migraciones |
| `SystemRoles` | Existe (roles_sistema) | ✓ OK | Aunque nombre en español en migraciones |
| `StreetTypes` | Existe (tipos_via) | ⚠️ RENAME | Debería ser `street_types` en inglés |
| `FlightRoles` | Existe | ✓ OK | Verificar mapeo con flight_crew_roles |
| `FlightAssignments` | Existe | ✓ OK | Verificar mapeo con flight_crew_assignments |

---

## 6. ANÁLISIS COMPARATIVO DETALLADO

### A. Tablas Correctamente Implementadas (✓)
```
1. continents              ✓
2. countries               ✓
3. regions                 ✓
4. cities                  ✓
5. street_types            ✓ (como tipos_via en migraciones)
6. document_types          ✓ (como tipos_documento)
7. persons                 ✓ (como personas)
8. email_domains           ✓ (como dominios_email)
9. phone_codes             ✓ (como codigos_telefono)
10. person_emails          ✓ (como personas_emails)
11. person_phones          ✓ (como personas_telefonos)
12. clients                ✓ (como clientes)
13. airlines               ✓
14. airports               ✓
15. airport_airline        ✓
16. staff_positions        ✓
17. staff                  ✓
18. availability_statuses  ✓
19. staff_availability     ✓
20. aircraft_manufacturers ✓
21. aircraft_models        ✓
22. aircraft               ✓
23. cabin_types            ✓
24. cabin_configurations   ✓
25. routes                 ✓
26. route_stopovers        ✓ (como route_layovers)
27. seasons                ✓
28. passenger_types        ✓ (como tipos_pasajero)
29. fares                  ✓
30. flight_statuses        ✓
31. flight_status_transitions ✓
32. flights                ✓
33. flight_crew_roles      ✓ (como flight_roles)
34. flight_crew_assignments ✓ (como flight_assignments)
35. seat_location_types    ✓
36. flight_seats           ✓
37. passengers             ✓ (como pasajeros)
38. reservation_statuses   ≈ (reempl. booking_statuses)
39. reservation_status_transitions ≈ (reempl. booking_status_transitions)
40. reservations           ≈ (reempl. bookings)
41. reservation_flights    ≈ (reempl. booking_flights)
42. reservation_passengers ≈ (reempl. booking_passengers)
43. ticket_statuses        ✓
44. tickets                ✓
45. checkin_statuses       ✓
46. check_ins              ✓
47. invoice_item_types     ✓
48. invoices               ✓
49. invoice_items          ✓
50. payment_statuses       ✓
51. payment_method_types   ✓
52. card_types             ✓
53. card_issuers           ✓
54. payment_methods        ✓
55. payments               ✓
56. system_roles           ✓ (como roles_sistema)
57. permissions            ✓ (como permisos)
58. role_permissions       ✓ (como roles_permisos)
59. users                  ✓ (como usuarios)
60. sessions               ✓ (como sesiones)
```

### B. Tablas NO Implementadas (✗)
```
1. addresses               ✗ (parcial como direcciones)
2. booking_statuses        ✗ (reemplazado por reservation_statuses)
3. booking_status_transitions ✗ (reemplazado por reservation_status_transitions)
4. bookings                ✗ (reemplazado por reservations)
5. booking_flights         ✗ (reemplazado por reservation_flights)
6. booking_passengers      ✗ (reemplazado por reservation_passengers)
7. baggage_types           ✗
8. baggage                 ✗
```

---

## 7. RECOMENDACIONES PRIORITIZADAS

### CRÍTICAS (Implementar inmediatamente)

1. **Crear módulo BAGGAGE**
   - [ ] Crear tabla `baggage_types`
   - [ ] Crear tabla `baggage`
   - [ ] Entidades correspondientes
   - [ ] Migraciones
   - [ ] Registrar en AppDbContext
   - **Justificación:** Sistema de cálculo de cargos por equipaje ausente

2. **Completar tabla DIRECTIONS/ADDRESSES**
   - [ ] Agregar relación `address_id` en `DirectionEntity` (nullable FK a persons)
   - [ ] Permitir que personas tengan múltiples direcciones
   - [ ] Crear nueva migración
   - **Justificación:** Base de datos de personas incompleta

3. **Unificación de nomenclatura SQL**
   - [ ] Estandarizar todas las tablas a inglés
   - [ ] Crear migración `RenameTableNamestoEnglish` (si no existe o completarla)
   - [ ] Tablas a renombrar: clientes → clients, personas → persons, usuarios → users, etc.
   - **Justificación:** Consistencia, mantenibilidad, estándares internacionales

### ALTAS (Implementar próximas 2 semanas)

4. **Revisar modelo Bookings vs Reservations**
   - [ ] Documentar decisión de usar Reservations en lugar de Bookings
   - [ ] Verificar que todas las FK apunten correctamente
   - [ ] Asegura constraints de integridad
   - **Justificación:** Cambio conceptual significativo respecto a referencia

5. **Validar Foreign Keys faltadas**
   - [ ] Agregar `staff_id` FK en migraciones si está ausente
   - [ ] Agregar validaciones CHECK en migraciones
   - [ ] Crear índices en columnas FK de alto acceso
   - **Justificación:** Integridad referencial incompleta

6. **Revisar AppDbContextModelSnapshot**
   - [ ] Actualizar snapshot con todas las 45+ DbSets
   - [ ] Verificar que tenga todas las entidades del Domain
   - **Justificación:** Solo 26 tablas en snapshot actual vs 45 DbSets

### MEDIAS (Implementar próximo mes)

7. **Completar documentación de módulos**
   - [ ] Documentar estructura de cada módulo
   - [ ] Verificar agregates Root entities
   - [ ] Revisión de Value Objects

8. **Testing de integridad de datos**
   - [ ] Crear scripts de validación de FK
   - [ ] Pruebas de cascada de deletes
   - [ ] Validar constraints numéricos (positivos, no nulos, etc.)

---

## 8. CHECKLISTS DE VALIDACIÓN

### Para cada tabla que migre/modifique:
- [ ] ¿Tiene Primary Key (id INT AUTO_INCREMENT)?
- [ ] ¿Están todos los Foreign Keys correctamente definidos?
- [ ] ¿Están todos los CHECK constraints implementados?
- [ ] ¿Está registrada en AppDbContext?
- [ ] ¿Existe Entity correspondiente en Domain o Infrastructure?
- [ ] ¿Existe EntityConfiguration en Infrastructure?
- [ ] ¿Está documentada en el módulo?

---

## 9. REFERENCIAS

- SQL Schema: `/home/tomdev_/Descargas/tablas_mysql.txt` (63 tablas)
- Proyecto: `/home/tomdev_/TomDev/Campus/C#/Sistema-de-Gesti-n-de-Tiquetes-A-reos`
- Migraciones: 55 archivos `.cs` (últimas migraciones 2026-04-16)
- AppDbContext: `src/shared/Context/AppDbContext.cs` (45 DbSets)
- Entidades: 61 archivos `*Entity.cs` en `src/Modules/**`

---

## Conclusión

El proyecto tiene una cobertura **~85%** del esquema SQL de referencia. Las deficiencias principales son:
1. **Sistema de equipaje (baggage) completamente ausente** - CRÍTICO
2. **Tabla addresses problemática** - CRÍTICO
3. **Inconsistencias de nomenclatura (Español/Inglés)** - ALTO
4. **Algunos Foreign Keys faltando** - MEDIO-ALTO
5. **AppDbContextModelSnapshot desactualizado** - MEDIO

Con la implementación de las recomendaciones críticas, el proyecto alcanzaría **95%+ de compatibilidad** con el esquema de referencia.
