# Informe Técnico: Módulo de Programa de Fidelización por Millas

## 1. Introducción

El presente informe técnico expone la arquitectura e implementación del módulo de fidelización por millas dentro del sistema de reservas de vuelos. Este componente integral permite recompensar la lealtad del cliente mediante la acumulación automatizada de millas por trayectos reservados y su subsecuente redención como mecanismo de descuento en futuras transacciones. El módulo cubre el ciclo de vida completo: acumulación al comprar/reservar, reversión automática por no abordaje y redención con reglas de negocio claras sobre cuál tiquete recibe el beneficio.

---

## 2. Diseño del Modelo de Datos

Para integrar las operaciones transaccionales del programa de fidelización garantizando la integridad de los datos, el modelo de dominio ha sido ampliado mediante las siguientes estructuras:

- **`Client` (Extensión):** Propiedad de navegación `ICollection<MilesTransaction>` para trazabilidad completa del historial de millas.
- **`MilesTransaction` (Nueva Entidad):** Libro mayor (*ledger*) inmutable *append-only* que registra todas las operaciones:
  - `Id` (PK): Identificador único de la transacción.
  - `ClientId` (FK): Referencia al cliente titular.
  - `ReservationId` (FK, Nullable): Trazabilidad hacia la reserva de origen.
  - `Amount`: Cuantía transaccional. Positivo = `Accumulation`; Negativo = `Redemption` o reversión.
  - `TransactionType`: Enumerador tipado (`Accumulation`, `Redemption`).
  - `TransactionDate`: Sello de tiempo UTC de la operación.
- **`Route` (Extensión):** Campo `Miles` (decimal) que almacena la distancia del trayecto en millas aéreas. Sirve como base para calcular cuántas millas otorga cada ruta.

**Relaciones:**
`MilesTransaction` mantiene restricciones de clave foránea hacia `Client` y `Reservation`, normalizando el modelo y previniendo anomalías de actualización.

---

## 3. Reglas de Negocio del Programa de Fidelización

### 3.1 Acumulación de millas por reserva

Las millas se acreditan **al momento de completar la compra o reserva del tiquete**, no al volar. El cálculo es:

```
Millas acreditadas = Route.Miles × cantidad de pasajeros
```

La distancia de cada ruta se almacena en el campo `Route.Miles` y se multiplica por **x10** respecto a la distancia real en kilómetros para llevar las cifras a una escala coherente con los tramos de redención:

| Ruta de ejemplo         | Distancia base | Miles × 10 por tiquete |
|-------------------------|----------------|------------------------|
| Bogotá → Medellín       | 215 km         | 2.150 millas           |
| Bogotá → Miami          | 2.760 km       | 27.600 millas          |
| Madrid → Londres        | 1.260 km       | 12.600 millas          |

Si el cliente cancela la reserva, las millas acreditadas son **revertidas** automáticamente mediante un registro negativo en `MilesTransaction`.

### 3.2 Reversión automática por reservas expiradas

Al iniciar el módulo de reservaciones (tanto el panel del cliente como el del agente), el sistema ejecuta un **barrido automático** de reservas pendientes cuyo vuelo ya ha despegado (hora actual UTC > `DepartureDate`):

1. Se marcan las reservas como **Perdida** (estado 3).
2. Se liberan los cupos de asiento del vuelo.
3. Se invoca `RevertMilesForReservationAsync` para retirar las millas acreditadas mediante un nuevo registro negativo en el ledger.

Todo el proceso ocurre dentro de una transacción atómica (`BeginTransactionAsync`) para garantizar consistencia.

> **ATENCIÓN:** El sistema muestra al cliente al finalizar cada reserva que si no completa el check-in antes del despegue, la reserva será marcada como perdida y los puntos ganados serán revocados.

### 3.3 Tramos de redención

El cliente puede redimir millas para obtener un descuento porcentual en su próximo vuelo según la siguiente tabla:

| Millas requeridas | Descuento otorgado       |
|-------------------|--------------------------|
| 25.000            | 10% de descuento         |
| 80.000            | 25% de descuento         |
| 150.000           | 50% de descuento         |
| 500.000           | 100% — Vuelo gratuito    |

Los tramos son progresivos: el cliente siempre accede al **descuento máximo que su saldo actual soporte**.

### 3.4 Regla de aplicación del descuento (un solo tiquete)

El descuento por millas se aplica **únicamente al primer tiquete** de la reserva. El resto de pasajeros en la misma reserva pagan el precio normal. Esto previene que una sola redención cubra múltiples tiquetes.

**Ejemplo — 3 pasajeros, precio base $720.000 c/u, descuento 50%:**

| Tiquete    | Precio base  | Descuento         | Subtotal     |
|------------|--------------|-------------------|--------------|
| Tiquete #1 | $720.000     | -50% (−$360.000)  | $360.000     |
| Tiquete #2 | $720.000     | —                 | $720.000     |
| Tiquete #3 | $720.000     | —                 | $720.000     |
| **TOTAL**  | $2.160.000   | −$360.000         | **$1.800.000** |

Al finalizar la reserva, el sistema muestra un recibo detallado con el desglose por tiquete, el ahorro aplicado, las millas utilizadas y las millas ganadas por el viaje.

### 3.5 Deducción de millas al redimir

Al confirmar la redención, las millas correspondientes al tramo elegido se descuentan del saldo del cliente mediante un registro negativo en `MilesTransaction` (`TransactionType = Redemption`). Esto ocurre **después del commit** exitoso de la reserva, garantizando atomicidad.

---

## 4. Interfaz de Usuario — Módulo "Redimir Millas"

En el perfil del cliente existe la opción **"Redimir Millas"** que presenta:

1. **Saldo actual** de millas acumuladas.
2. **Tabla de descuentos** con estado `Disponible` / `Insuficiente` según el saldo.
3. **Descuento máximo disponible** resaltado en pantalla.
4. **Selección del tramo** mediante menú interactivo (solo se muestran los tramos alcanzables).
5. Al confirmar, se inicia directamente el flujo de reserva de vuelo con el descuento activo.

---

## 5. Reportes Analíticos con LINQ

Las consultas priorizan el rendimiento: evitan el problema N+1, delegan cómputo al motor SQL mediante proyecciones (`Select`) y agregaciones (`GroupBy`), y usan `.AsNoTracking()` para minimizar la carga del *Change Tracker*.

### Clientes con más millas acumuladas

```csharp
var topAccumulators = await _context.MilesTransactions
    .AsNoTracking()
    .Where(t => t.TransactionType == TransactionType.Accumulation)
    .GroupBy(t => new { t.ClientId, t.Client.Person.FirstName, t.Client.Person.LastName })
    .Select(g => new ClientMilesDTO
    {
        ClientName = $"{g.Key.FirstName} {g.Key.LastName}",
        TotalAccumulated = g.Sum(t => t.Amount)
    })
    .OrderByDescending(c => c.TotalAccumulated)
    .Take(10)
    .ToListAsync();
```

### Clientes que más redimen millas

```csharp
var topRedeemers = await _context.MilesTransactions
    .AsNoTracking()
    .Where(t => t.TransactionType == TransactionType.Redemption)
    .GroupBy(t => new { t.ClientId, t.Client.Person.FirstName, t.Client.Person.LastName })
    .Select(g => new ClientRedemptionDTO
    {
        ClientName = $"{g.Key.FirstName} {g.Key.LastName}",
        TotalRedeemed = g.Sum(t => Math.Abs(t.Amount))
    })
    .OrderByDescending(c => c.TotalRedeemed)
    .Take(10)
    .ToListAsync();
```

### Aerolíneas con mayor volumen de fidelización

```csharp
var topAirlinesByLoyalty = await _context.MilesTransactions
    .AsNoTracking()
    .Where(t => t.TransactionType == TransactionType.Accumulation && t.Reservation != null)
    .GroupBy(t => new { t.Reservation.Flight.AirlineId, t.Reservation.Flight.Airline.Name })
    .Select(g => new AirlineLoyaltyDTO
    {
        AirlineName = g.Key.Name,
        TotalMilesGranted = g.Sum(t => t.Amount)
    })
    .OrderByDescending(x => x.TotalMilesGranted)
    .ToListAsync();
```

### Rutas con mayor acumulación de millas

```csharp
var topRoutesByMiles = await _context.MilesTransactions
    .AsNoTracking()
    .Where(t => t.TransactionType == TransactionType.Accumulation && t.Reservation != null)
    .GroupBy(t => new { t.Reservation.Flight.RouteId,
                        Origin = t.Reservation.Flight.Route.OriginAirport.Name,
                        Destination = t.Reservation.Flight.Route.DestinationAirport.Name })
    .Select(g => new RouteMilesDTO
    {
        Origin = g.Key.Origin,
        Destination = g.Key.Destination,
        TotalMiles = g.Sum(t => t.Amount)
    })
    .OrderByDescending(x => x.TotalMiles)
    .Take(5)
    .ToListAsync();
```

### Ranking de viajeros frecuentes

El ranking considera como "vuelo completado" toda reserva en estado **Confirmada** (`ReservationStatusId == 2`), ya que en este sistema el check-in equivale a la confirmación del viaje.

```csharp
var frequentFlyersRanking = await _context.Clients
    .AsNoTracking()
    .Select(c => new FrequentFlyerDTO
    {
        ClientName = $"{c.Person.FirstName} {c.Person.LastName}",
        CompletedFlights = c.Reservations.Count(r => r.ReservationStatusId == 2),
        CurrentBalance = c.MilesTransactions.Sum(t => t.Amount)
    })
    .Where(c => c.CompletedFlights > 0)
    .OrderByDescending(c => c.CompletedFlights)
    .ThenByDescending(c => c.CurrentBalance)
    .ToListAsync();
```

---

## 6. Integración con el Sistema Principal

La cohesión operativa entre el motor de reservas y el módulo de lealtad exige rigor transaccional (ACID):

- **Reserva con descuento:** La reserva y la deducción de millas se ejecutan bajo el mismo contexto transaccional. Si ocurre un fallo, el *rollback* garantiza que no se cobre descuento sin reserva confirmada.
- **Acreditación de millas:** Se realiza inmediatamente tras el commit exitoso de la reserva. Las millas se calculan como `Route.Miles × n_pasajeros`.
- **Reversión por expiración:** Al entrar al módulo de reservaciones, el sistema ejecuta `RevokeExpiredReservationsAsync` (cliente) y `RevokeAllExpiredReservationsAsync` (agente), asegurando consistencia del estado antes de mostrar cualquier información al usuario.
- **Recibo final:** Tras cada reserva se muestra un recibo en caja verde con desglose por tiquete, indicando explícitamente a cuál tiquete se aplicó el descuento y cuántas millas fueron ganadas y/o utilizadas.

---

## 7. Conclusión

El ecosistema de fidelización implementado cubre el ciclo de vida completo de los puntos por vuelo: acumulación inmediata al reservar, reversión automática ante incumplimiento del abordaje, redención con reglas de negocio precisas (descuento únicamente sobre el primer tiquete), y validación preventiva del estado de las reservas al iniciar cada sesión. El diseño de inmutabilidad tipo *ledger*, combinado con transacciones atómicas y proyecciones LINQ optimizadas, garantiza consistencia, trazabilidad y rendimiento en todos los flujos del módulo.
